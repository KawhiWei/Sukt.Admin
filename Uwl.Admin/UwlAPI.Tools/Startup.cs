using AutoMapper;
using UwlAPI.Tools.AuthHelper.JWT;
using UwlAPI.Tools.AuthHelper.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Text;
using Uwl.Common.AutoMapper;
using Uwl.Data.EntityFramework.LogsServives;
using Uwl.Data.EntityFramework.MenuServices;
using Uwl.Data.EntityFramework.UserServices;
using Uwl.Data.EntityFramework.Uwl_DbContext;
using Uwl.Data.Model.BaseModel;
using Uwl.Data.Server.LogsServices;
using Uwl.Data.Server.MenuServices;
using Uwl.Data.Server.UserServices;
using Uwl.Domain.LogsInterface;
using Uwl.Domain.MenuInterface;
using Uwl.Domain.UserInterface;
using Uwl.Domain.RoleInterface;
using Uwl.Data.Server.RoleServices;
using Uwl.Cache.Redis;
using Uwl.Data.EntityFramework.ButtonServices;
using Uwl.Domain.ButtonInterface;
using Uwl.Data.Server.ButtonServices;
using Uwl.Data.Server.RoleAssigServices;
using Uwl.Data.EntityFramework.RoleServives;
using Microsoft.AspNetCore.Authorization;
using UwlAPI.Tools.AuthHelper.Policys;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using UwlAPI.Tools.Filter;
using Uwl.Common.GlobalRoute;
using Uwl.Domain.IRepositories;
using Uwl.Data.EntityFramework.RepositoriesBase;
using Microsoft.AspNetCore.Mvc;
using SignalRDemo.SignalrHubs;
using Uwl.Data.Model;
using Uwl.Data.EntityFramework.OrganizeServives;
using Uwl.Domain.OrganizeInterface;
using Uwl.Data.Server.OrganizeServices;
using Uwl.Common.RabbitMQ;
using Uwl.QuartzNet.JobCenter.Center;

namespace UwlAPI.Tools
{
    /// <summary>
    /// 启动程序
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 定义一个启用数据库连接IConfiguration的属性
        /// </summary>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// autoMapper接口
        /// </summary>
        private MapperConfiguration _mapperConfiguration { get; set; }
        /// <summary>
        /// 在构造函数中操作appsettings文件获取数据库连接字符串
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// 启动程序
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            #region 配置启动程序
            //获取appsettings.json文件Default节点下面的连接字符串
            var sqlconn = Configuration.GetConnectionString("SqlserverDefault");
            //第一个参数传入连接字符串 ，     第二个参数指明执行迁移的程序集
            //options.UseSqlServer(sqlconn,b=>b.MigrationsAssembly("CorePractice")   如果事WebApi 项目的话需要加services.AddEntityFrameworkSqlServer().AddDbContext
            services.AddEntityFrameworkSqlServer().AddDbContext<UwlDbContext>(options => 
                options.UseSqlServer(sqlconn, b => b.MigrationsAssembly("UwlAPI.Tools")));
            #endregion
            services.AddMvc(mvc =>
            {
                //全局路由权限公约，给路由添加Authorize特性
                mvc.Conventions.Insert(0, new GlobalRouteAuthorizeConvention());
                //mvc.Conventions.Insert(0, new AddRoutePrefixFilter(new RouteAttribute(RoutePrefix.Name)));
            });
            services.AddSignalR();
            #region Swagger
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info
                {
                    Version = "v0.1.0",
                    Title = "UwlAPI.Tools",
                    Description = "框架说明文档",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "UwlAPI.Tools", Email = "UwlAPI.Tools@xxx.com", Url = "https://www.jianshu.com/u/94102b59cc2a" }
                });
                var basepath = AppDomain.CurrentDomain.BaseDirectory;
                var xmls = System.IO.Directory.GetFiles(basepath, "*.xml");
                foreach (var xml in xmls)
                {
                    x.IncludeXmlComments(xml);
                }
                var IssuerName = (Configuration.GetSection("JwtSettings"))["Issuer"];// 发行人
                var security = new Dictionary<string, IEnumerable<string>> { { IssuerName, new string[] { } }, };
                x.AddSecurityRequirement(security);
                x.AddSecurityDefinition(IssuerName, new ApiKeyScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = "header",//jwt默认存放Authorization信息的位置(请求头中)
                    Type = "apiKey"
                });

            });
            #endregion

            #region 获取配置文件信息
            var audienceConfig = Configuration.GetSection("JwtSettings");
            var symmetricKeyAsBase64 = audienceConfig["SecretKey"];
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);    
            var jwtSettings = new JwtSettings();
            Configuration.Bind("JwtSettings", jwtSettings);
            //获取主要jwt token参数设置   // 令牌验证参数
            var TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                //Token颁发机构
                ValidIssuer = audienceConfig["Issuer"],
                //颁发给谁
                ValidAudience = audienceConfig["Audience"],
                //这里的key要进行加密，需要引用Microsoft.IdentityModel.Tokens
                IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,////是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比
                ClockSkew = TimeSpan.Zero,////允许的服务器时间偏移量
                RequireExpirationTime = true,
            };
            #endregion

            #region No.1     官方的JWT验证 简单的策略授权（简单版） 
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("Client", policy => policy.RequireRole("Client").Build());
            //    options.AddPolicy("Admin", policy => policy.RequireRole("Admin").Build());
            //    options.AddPolicy("SystemOrAdmin", policy => policy.RequireRole("Admin", "System"));
            //    options.AddPolicy("SystemOrAdminOrOther", policy => policy.RequireRole("Admin", "System", "Other"));
            //});
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(jwt=>
            //{
            //    jwt.TokenValidationParameters = TokenValidationParameters;
            //});
            #endregion

            #region No.2    中间件授权认证方式 使用此认证需要在Configure里面放开app.UseMiddleware<JwtTokenAuth>();取消注释所有控制器的[Authorize(Policy = "Admin")] 并且注释掉[Authorize]
            //中间件签名过期无效待解决——————需要自己写鉴权方式，前名是否过期……？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？？
            //基于角色的策略授权（简单版） + 自定义认证中间件
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("Client", policy => policy.RequireRole("Client").Build());
            //    options.AddPolicy("Admin", policy => policy.RequireRole("Admin").Build());
            //    options.AddPolicy("AdminOrClient", policy => policy.RequireRole("AdminOrClient").Build());
            //});
            // 2【认证】、然后在下边的configure里，配置中间件即可:
            // app.UseMiddleware<JwtTokenAuth>();

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("Client", policy => policy.RequireRole("Client").Build());
            //    options.AddPolicy("Admin", policy => policy.RequireRole("Admin").Build());
            //    options.AddPolicy("SystemOrAdmin", policy => policy.RequireRole("Admin", "System"));
            //    options.AddPolicy("SystemOrAdminOrOther", policy => policy.RequireRole("Admin", "System", "Other"));
            //});
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(jwt =>
            //{
            //    jwt.TokenValidationParameters = TokenValidationParameters;
            //});
            #endregion

            #region No.3    复杂策略授权 + 官方JWT认证
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var permission = new List<PermissionItem>(); //需要从数据库动态绑定，这里先留个空，后边处理器里动态赋值
            //
            var permissionRequirement = new PermissionRequirement
                (
                    "/api/denied",//拒绝跳转的Action
                    permission,//角色菜单实体
                    ClaimTypes.Role,//基于角色的授权
                    jwtSettings.Issuer,//发行人
                    jwtSettings.Audience,//听众
                    signingCredentials,//签名凭据
                    expiration:TimeSpan.FromSeconds(15*60)//过期时间
                );
            //No.1 基于角色的授权
            services.AddAuthorization(options =>
            {
                options.AddPolicy(GlobalRouteAuthorizeVars.Name, policy => policy.Requirements.Add(permissionRequirement));
            });
            //No.2 配置认证服务
            services.AddAuthentication(options =>
            {
                //认证middleware配置
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(t =>
            {
                t.TokenValidationParameters = TokenValidationParameters;
                //过期时间判断
                //t.Events = new JwtBearerEvents
                //{
                //    // 如果过期，则把<是否过期>添加到，返回头信息中
                //    OnAuthenticationFailed = context =>
                //    {
                //        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                //        {
                //            context.Response.Headers.Add("Token-Expired", "true");
                //        }
                //        return Task.CompletedTask;
                //    }
                //};
            });

            //注入权限处理核心控制器,将自定义的授权处理器 匹配给官方授权处理器接口，这样当系统处理授权的时候，就会直接访问我们自定义的授权处理器了。
            services.AddScoped<IAuthorizationHandler, PermissionHandler>();//注意此处的注入类型取决于你获取角色Action信息的注入类型如果你服务层用AddScoped此处也必须是AddScoped
            //将授权必要类注入生命周期内
            services.AddSingleton(permissionRequirement);

            #endregion

            #region 添加automapper实体映射,如果存在相同字段则自动映射
            services.AddAutoMapper();
            //注册需要自动映射的实体类
            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                //初始化自动映射类
                cfg.AddProfile<MyProfile>();
            });
            //将自动映射属性封装为静态属性
            MyMappers.ObjectMapper = _mapperConfiguration.CreateMapper();
            #endregion

            #region Cors跨域需要添加以下代码
            //services.AddCors(c =>
            //{
            //    //控制器中[EnableCors("AllRequests")]名字需对应
            //    c.AddPolicy("AllRequests", policy =>
            //    {
            //        policy
            //        .AllowAnyOrigin()//允许任何源
            //        .AllowAnyMethod()//允许任何方式
            //        .AllowAnyHeader()//允许任何头
            //        .AllowCredentials();//允许cookie
            //    });
            //});
            #endregion

            #region 接口控制反转依赖注入      -netcore自带方法

            //services.AddSingleton<IAuthorizationHandler, DomainUserServer>();

            services.AddScoped<MyProfile>();//注入自动映射类

            //注入工作单元接口和实现
            services.AddScoped<IUnitofWork, UnitofWorkBase>();


            //services.AddScoped(typeof(IRepository<Entity,Guid>), typeof(UwlRepositoryBase<Entity, Guid>));

            //注入用户管理领域仓储层实现
            services.AddScoped<IUserRepositoty, DomainUserServer>();
            //注入用户管理服务层实现
            services.AddScoped<IUserServer, UserServer>();
            //services.AddScoped<Log>();//注入记录日志类

            //注入日志管理领域仓储层实现
            services.AddScoped<ILogRepositoty, DomainLogsServer>();
            //注入日志管理服务层实现
            services.AddScoped<ILogsServer, LogsServer>();
            //注入菜单管理领域仓储层实现
            services.AddScoped<IMenuRepositoty, DomainMenuServer>();
            //注入菜单管理服务层实现
            services.AddScoped<IMenuServer, MenuServer>();

            //注入角色管理领域仓储层实现
            services.AddTransient<IRoleRepositoty, DomainRoleServer>();
            //注入角色管理服务层实现
            services.AddTransient<IRoleServer, RoleServer>();
            //注入按钮管理领域仓储层实现
            services.AddScoped<IButtonRepositoty, DomainButtonServer>();
            //注入按钮管理服务层实现
            services.AddScoped<IButtonServer, ButtonServer>();

            //注入菜单按钮管理领域仓储层实现
            services.AddScoped<ISysMenuButton, DomainSysMenuButton>();
            //注入菜单按钮服务层实现
            services.AddScoped<ISysMenuButtonServer, SysMenuButtonServer>();

            //注入角色权限分配领域仓储实现
            services.AddScoped<IRoleRightAssigRepository, DomainRoleRightAssigServer>();
            //注入角色权限分配服务层实现
            services.AddScoped<IRoleAssigServer, SysRoleAssigServer>();

            //注入用户角色领域仓储实现
            services.AddScoped<IUserRoleRepository, DomainUserRoleServer>();
            //注入用户角色服务层
            services.AddScoped<IUserRoleServer, UserRoleServer>();

            //注入组织机构领域仓储实现
            services.AddScoped<IOrganizeRepositoty, DomainOrganizeServer>();
            //注入组织机构服务层
            services.AddScoped<IOrganizeServer, OrganizeServer>();

            //注入Redis缓存
            services.AddSingleton<IRedisCacheManager, RedisCacheManager>();
            //注入RabbitMQ服务
            services.AddSingleton<IRabbitMQ, RabbitServer>();
            services.AddSingleton<ISchedulerCenter, SchedulerCenterServer>();
            #endregion


        }
        /// <summary>
        /// 应用程序管道
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            //在此处加入允许跨域
            //app.UseCors("AllRequests"); //跨域第二种方法，使用策略，详细策略信息在ConfigureService中//loggerFactory.AddConsole();
            #region Environment
            //判断是否是环境变量
            if (env.IsDevelopment())
            {
                //开发环境中发生异常抛出此例外
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // 在非开发环境中，使用HTTP严格安全传输(or HSTS) 对于保护web安全是非常重要的。
                // 强制实施 HTTPS 在 ASP.NET Core，配合 app.UseHttpsRedirection
                //app.UseHsts();
                //生产环境中发生异常跳转到指定控制器
                //app.UseHsts();
            }
            #endregion
            //<!--------------------------------------《重要》不管怎么配置，所有的东西必须放到MVC前面-------------------------------------->
            //中间件认证必须在MVC前面
            #region Authen
            //注意此授权方法已经放弃，请使用下边的官方验证方法。但是如果你还想传User的全局变量，还是可以继续使用中间件
            //app.UseMiddleware<JwtTokenAuth>();//启动中间件认证
            app.UseAuthentication();//添加官方认证
            #endregion


            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v1/swagger.json", "I_Sfront.Core API");
                c.RoutePrefix = "";
            });
            #endregion
            app.UseStaticFiles();// 使用静态文件
            app.UseCookiePolicy();// 使用cookie
            //app.UseHttpsRedirection();// 跳转https
            app.UseStatusCodePages();
            app.UseMvc();
            app.UseSignalR(routes =>
            {
                routes.MapHub<SignalRChatHub>("/api/chatHub");
            });
        }
    }
}

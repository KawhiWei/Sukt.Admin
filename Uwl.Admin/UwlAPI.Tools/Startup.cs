using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Uwl.Common.AutoMapper;
using Uwl.Data.EntityFramework.Uwl_DbContext;
using System.Collections.Generic;
using UwlAPI.Tools.Filter;
using Uwl.Common.SignalRMessage;
using UwlAPI.Tools.Extensions;
using AutoMapper;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using UwlAPI.Tools.StartupExtension;
using Uwl.Common.LogsMethod;

namespace UwlAPI.Tools
{
    /// <summary>
    /// 启动程序
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// SignalR跨域处理
        /// </summary>
        public const string CorsName = "SignalRCors";
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

            #region Cors跨域需要添加以下代码
            //services.AddCors(c =>
            //{
            //    //控制器中[EnableCors("AllRequests")]名字需对应
            //    c.AddPolicy(CorsName,
            //        policy => policy
            //        .AllowAnyOrigin()
            //        .AllowAnyMethod()//允许任何方式
            //        .AllowAnyHeader());//允许任何头//允许cookie
            //});

            //services.AddCors(op => 
            //{ op.AddPolicy("cors", 
            //    set => { set.SetIsOriginAllowed(origin => true)
            //        .AllowAnyHeader().AllowAnyMethod().AllowCredentials(); }); });

            #endregion
            services.AddSignalR();

            #region 配置启动程序
            //获取appsettings.json文件Default节点下面的连接字符串
            var sqlconn = Configuration.GetConnectionString("SqlserverDefault");
            LogServer.WriteLog("20190107", "数据库链接字符串", sqlconn);
            //第一个参数传入连接字符串 ，     第二个参数指明执行迁移的程序集
            //options.UseSqlServer(sqlconn,b=>b.MigrationsAssembly("CorePractice")   如果事WebApi 项目的话需要加services.AddEntityFrameworkSqlServer().AddDbContext
            services.AddEntityFrameworkSqlServer().AddDbContext<UwlDbContext>(options => 
                options.UseSqlServer(sqlconn, b => b.MigrationsAssembly("UwlAPI.Tools")));
            #endregion
            services.AddControllers(mvc =>
            {
                //全局路由权限公约，给路由添加Authorize特性
                mvc.Conventions.Insert(0, new GlobalRouteAuthorizeConvention());
                //mvc.Conventions.Insert(0, new AddRoutePrefixFilter(new RouteAttribute(RoutePrefix.Name)));
            });

            #region Swagger
            services.SwaggerConfigureExtension();
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
            services.AddHttpContextExtension();
            services.AuthExtension();
            #endregion
            #region 添加automapper实体映射,如果存在相同字段则自动映射
            services.AddAutoMapper(GetType());
            //注册需要自动映射的实体类
            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                //初始化自动映射类
                cfg.AddProfile<MyProfile>();
            });
            //将自动映射属性封装为静态属性
            MyMappers.ObjectMapper = _mapperConfiguration.CreateMapper();
            services.AddScoped<MyProfile>();//注入自动映射类
            #endregion

            #region 接口控制反转依赖注入      -netcore自带方法
            services.ServerExtension();
            services.RepositotyExtension();
            #endregion

            #region 缓存和任务调度中心使用 单例模式注入生命周期
            services.CommonExtension();
            services.JobExtension();



            #endregion

        }

        /// <summary>
        /// 应用程序管道
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {

            //在此处加入允许跨域
            //app.UseCors(CorsName).UseSignalR(routes =>
            //{
            //    routes.MapHub<SignalRChat>("/api2/chatHub");

            //}); ; //跨域第二种方法，使用策略，详细策略信息在ConfigureService中//loggerFactory.AddConsole();
            //app.UseWebSockets();
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
            #endregion

            #region Swagger
            app.SwaggerAppConfigure();
            #endregion


            app.UseStaticFiles();// 使用静态文件
            app.UseCookiePolicy();// 使用cookie
            //app.UseHttpsRedirection();// 跳转https
            app.UseStatusCodePages();
            
            app.UseLog();
            app.UseRouting();//路由中间件
            app.UseAuthentication();//添加官方认证.
            // 然后是授权中间件
            app.UseAuthorization();
            // 短路中间件，配置Controller路由
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHub<SignalRChat>("/api2/chatHub");
            });
            //app.AutoJob();
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Sukt.Core.Domain.Models;
using Sukt.Module.Core.Extensions;
using System;
using System.Linq;
using System.Security.Principal;
using Sukt.Core.EntityFrameworkCore;
using Sukt.Module.Core.Modules;
using Sukt.AspNetCore;
using Sukt.Module.Core.AppOption;
using Sukt.Module.Core.SuktDependencyAppModule;
using Sukt.Module.Core.Events;
using Sukt.Swagger;
using Sukt.AutoMapper;
using SuktCore.Aop;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Collections.Generic;
using Sukt.WebSocketServer;
using Sukt.WebSocketServer.MvcHandler;

namespace Sukt.Core.API.Startups
{
    [SuktDependsOn(
        typeof(AopModule),
        typeof(SuktAutoMapperModuleBase),
        //typeof(CSRedisModuleBase),
        typeof(IdentityModule),
        typeof(SuktSwaggerModule),
        typeof(DependencyAppModule),
        typeof(EventBusAppModuleBase),
        typeof(EntityFrameworkCoreModule),
        //typeof(MongoDBModule),
        //typeof(MultiTenancyModule),
        typeof(MigrationModuleBase),
        typeof(RedisModule)
        )]
    public class SuktAppWebModule : SuktAppModule
    {
        private string _corePolicyName = string.Empty;
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            var service = context.Services;
            service.AddControllers(x =>
            {
                x.SuppressAsyncSuffixInActionNames = false;
                //x.Filters.Add<PermissionAuthorizationFilter>();
                x.Filters.Add<AuditLogFilter>();
            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });
            context.Services.AddFileProvider();
            var configuration = service.GetConfiguration();
            if (configuration == null)
            {
                IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
                configuration = configurationBuilder.Build();
                context.Services.AddSingleton<IConfiguration>(configuration);
            }
            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath; //获取项目路径
            context.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(basePath));
            service.Configure<AppOptionSettings>(configuration.GetSection("SuktCore"));
            AppOptionSettings option = new AppOptionSettings();
            if (configuration != null)
            {
                configuration.Bind("SuktCore", option);
                context.Services.AddObjectAccessor(option);
                context.Services.Configure<AppOptionSettings>(o =>
                {
                    o.AuditEnabled = option.AuditEnabled;
                    o.Auth = option.Auth;
                    o.Cors = option.Cors;
                    o.DbContexts = option.DbContexts;
                    o.Jwt = option.Jwt;
                });
            }
            var settings = service.GetAppSettings();
            service.AddTransient<IPrincipal>(provider =>
            {
                IHttpContextAccessor accessor = provider.GetService<IHttpContextAccessor>();
                return accessor?.HttpContext?.User;
            });
            service.AddSuktWebSocketConfigRouterEndpoint(x =>
            {

                x.WebSocketChannels = new Dictionary<string, WebSocketRouteOption.WebSocketChannelHandler>()
                {
                    { "/im",new MvcChannelHandler(4*1024).ConnectionEntry}
                };
                x.ApplicationServiceCollection = service;
            });

            if (!settings.Cors.PolicyName.IsNullOrEmpty() && !settings.Cors.Url.IsNullOrEmpty()) //添加跨域
            {
                _corePolicyName = settings.Cors.PolicyName;
                service.AddCors(c =>
                {
                    c.AddPolicy(settings.Cors.PolicyName, policy =>
                    {
                        policy.WithOrigins(settings.Cors.Url
                          .Split(",", StringSplitOptions.RemoveEmptyEntries).ToArray())
                        //policy.WithOrigins("http://localhost:5001")//支持多个域名端口，注意端口号后不要带/斜杆：比如localhost:8000/，是错的
                        .AllowAnyHeader().AllowAnyMethod().AllowCredentials();//允许cookie;
                    });
                });
            }
        }
        public override void ApplicationInitialization(ApplicationContext context)
        {
            var applicationBuilder = context.GetApplicationBuilder();
            if (!_corePolicyName.IsNullOrEmpty())
            {
                applicationBuilder.UseCors(_corePolicyName); //添加跨域中间件
            }
            #region WebSocket
            var webSocketOptions = new WebSocketOptions()
            {
                KeepAliveInterval = TimeSpan.FromSeconds(15),//服务的主动向客户端发起心跳检测时间
                ReceiveBufferSize = 4 * 1024//数据缓冲区
            };
            applicationBuilder.UseWebSockets(webSocketOptions);
            applicationBuilder.UseSuktWebSocketServer(applicationBuilder.ApplicationServices);
            #endregion
            applicationBuilder.UseRouting();
            applicationBuilder.UseAuthentication();//授权
            applicationBuilder.UseAuthorization();//认证
            applicationBuilder.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

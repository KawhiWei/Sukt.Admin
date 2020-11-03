using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Sukt.Core.Shared.AppOption;
using Sukt.Core.Shared.Extensions;
using Sukt.Core.Shared.Modules;
using System;
using System.Threading.Tasks;

namespace Sukt.Core.API.Startups
{
    public class IdentityServerAuthModule : SuktAppModule
    {
        public override void ApplicationInitialization(ApplicationContext context)
        {
        }

        public override void ConfigureServices(ConfigureServicesContext context)
        {
            var service = context.Services;
            AppOptionSettings settings = service.GetAppSettings();
            service.AddHttpContextAccessor();
            service.AddAuthorization();
            service.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt =>
            {
                Console.WriteLine($"{settings.Auth?.Authority}+++++++++++++++++++++{settings.Auth?.Audience}");

                jwt.Authority = settings.Auth?.Authority ?? "http://10.1.40.210:8042";
                jwt.Audience = settings.Auth?.Audience ?? "IDN.Services.BasicsService.API";
                jwt.RequireHttpsMetadata = false;
                jwt.Events = new JwtBearerEvents /*jwt自带事件*/
                {
                    OnAuthenticationFailed = context =>
                    {

                        // 如果过期，则把<是否过期>添加到，返回头信息中
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }

                };
            });
        }
    }
}

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Sukt.Core.Domain.Models;
using Sukt.Core.EntityFrameworkCore.Repositories;
using Sukt.Core.Identity;
using Sukt.Module.Core.Extensions;
using System;
using System.Threading.Tasks;

namespace Sukt.Core.API
{
    public class IdentityModule : IdentityModuleBase<UserStore, RoleStore, User, UserRoleEntity, RoleEntity, Guid, Guid>
    {

        protected override void AddAuthentication(IServiceCollection services)
        {
            var settings = services.GetAppSettings();
            var jwt = settings.Jwt;
            services.AddAuthorization();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddSuktAuthServerAuthentication(x =>
            {
                x.Authority = "https://localhost:5007";
                x.RequireHttpsMetadata = false;
                x.ApiName = "sukt.admin.react";
                //x.Authority = "https://localhost:5001";
                //x.RequireHttpsMetadata = false;
                //x.ApiName = "resource1";
            });

            //    .AddJwtBearer(jwt =>
            //{
            //    jwt.Authority = "https://localhost:5007";
            //    jwt.Audience = "sukt.admin.react";
            //    jwt.RequireHttpsMetadata = false;
            //    //jwt.TokenValidationParameters = new TokenValidationParameters() { ValidateAudience = false };
            //    jwt.Events = new JwtBearerEvents /*jwt自带事件*/
            //    {
            //        OnAuthenticationFailed = context =>
            //        {
            //            // 如果过期，则把<是否过期>添加到，返回头信息中
            //            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            //            {
            //                context.Response.Headers.Add("Token-Expired", "true");
            //            }
            //            return Task.CompletedTask;
            //        }
            //    };
            //});
        }
        protected override Action<IdentityOptions> IdentityOption()
        {
            return options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = false;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            };
        }
        protected override IdentityBuilder UseIdentityBuilder(IdentityBuilder identityBuilder)
        {
            return identityBuilder.AddDefaultTokenProviders();
        }
    }
}

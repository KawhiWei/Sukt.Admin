using AutoMapper.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Uwl.Common.GlobalRoute;
using Uwl.Common.Helper;
using UwlAPI.Tools.AuthHelper.JWT;
using UwlAPI.Tools.AuthHelper.Policys;

namespace UwlAPI.Tools.StartupExtension
{
    /// <summary>
    /// 
    /// </summary>
    public static class AuthorizationExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public static void AuthExtension(this IServiceCollection services)
        {
            #region 获取配置文件信息
            var audienceConfig = Appsettings.app(new string[] { "JwtSettings"});// Configuration.GetSection("JwtSettings");
            var jwtSettings = new JwtSettings()
            {
                SecretKey = Appsettings.app(new string[] { "JwtSettings", "SecretKey" }),
                Issuer = Appsettings.app(new string[] { "JwtSettings", "Issuer" }),
                Audience = Appsettings.app(new string[] { "JwtSettings", "Audience" }),

            };

            var symmetricKeyAsBase64 = jwtSettings.SecretKey;
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);
            
            //Configuration.Bind("JwtSettings", jwtSettings);
            //获取主要jwt token参数设置   // 令牌验证参数
            var TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                //Token颁发机构
                ValidIssuer = jwtSettings.Issuer,
                //颁发给谁
                ValidAudience = jwtSettings.Audience,
                //这里的key密钥要进行加密，需要引用Microsoft.IdentityModel.Tokens
                IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),//加密密钥
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,////是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比
                ClockSkew = TimeSpan.Zero,////允许的服务器时间偏移量
                RequireExpirationTime = true,
            };
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
                    expiration: TimeSpan.FromSeconds(60 *1)//过期时间
                );
            //No.1 基于自定义角色的策略授权
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

                //给SignalR 赋值给集线器的链接管道添加Token验证
                t.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(path) && path.StartsWithSegments("/api2/chatHub"))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
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
        }
    }
}

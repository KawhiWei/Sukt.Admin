using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Sukt.AuthServer.Constants;
using Sukt.AuthServer.Domain.SuktAuthServer;
using Sukt.AuthServer.Domain.SuktAuthServer.SuktApplicationStore;
using Sukt.AuthServer.EndpointHandler;
using Sukt.AuthServer.EndpointRouterHandler;
using Sukt.AuthServer.Generator;
using Sukt.AuthServer.Validation;
using Sukt.AuthServer.Validation.SecretValidates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Endpoint = Sukt.AuthServer.EndpointRouterHandler.Endpoint;

namespace Sukt.AuthServer.Extensions
{
    /// <summary>
    /// 默认处理器注册到DI容器内
    /// </summary>
    public static class SuktAuthServerExtension
    {
        public static IServiceCollection AddSuktAuthServer(this IServiceCollection service)
        {

            service.AddDefaultEndpoints()
                .AddValidationServices()
                .AddResponseGenerators()
                .AddDefaultSecretParsers()
                .AddDefaultService()
                .AddClientStore<SuktApplicationStore>()
                .AddResourceStore<SuktResourceScopeStore>()
                .TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            ;

            return service;
        }
        public static IServiceCollection AddResourceStore<T>(this IServiceCollection services) where T : class, ISuktResourceScopeStore
        {
            services.AddTransient<ISuktResourceScopeStore, T>();
            return services;
        }
        /// <summary>
        /// 默认断点路由器注册
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static IServiceCollection AddDefaultEndpoints(this IServiceCollection service)
        {
            service.AddTransient<IEndpointRouter, EndpointRouter>();
            service.AddEndPoint<AuthorizeEndpoint>(EndpointNames.Authorize, ProtocolRoutePaths.Authorize.EnsureLeadingSlash());
            service.AddEndPoint<TokenEndpoint>(EndpointNames.Authorize, ProtocolRoutePaths.Token.EnsureLeadingSlash());
            return service;
        }
        /// <summary>
        /// 注册端点路由处理器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="service"></param>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static IServiceCollection AddEndPoint<T>(this IServiceCollection service,string name,PathString path)where T: class, IEndpointHandler
        {
            service.AddTransient<T>();
            service.AddSingleton(new Endpoint(name, path, typeof(T)));
            return service;
        }
        public static IServiceCollection AddClientStore<T>(this IServiceCollection service) where T :class , ISuktApplicationStore
        {
            service.AddTransient<ISuktApplicationStore, T>();
            return service;
        }
        public static IServiceCollection AddResourceOwnerValidator<T>(this IServiceCollection service) where T : class, IResourceOwnerPasswordValidator
        {
            service.AddTransient<IResourceOwnerPasswordValidator, T>();
            return service;
        }
        /// <summary>
        /// 注入验证服务
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static IServiceCollection AddValidationServices(this IServiceCollection service)
        {
            service.AddTransient<IClientSecretValidator, ClientSecretValidator>();
            service.AddTransient<IResourceValidator, DefaultResourceValidator>();
            service.AddTransient<ITokenRequestValidator, TokenRequestValidator>();
            service.AddTransient<ISecretValidator, HashedSharedSecretValidator>();
            service.TryAddTransient<IResourceOwnerPasswordValidator, NotSupportedResourceOwnerPasswordValidator>();
            return service;
        }
        public static IServiceCollection AddResponseGenerators(this IServiceCollection service)
        {
            service.AddTransient<ITokenResponseGenerator, TokenResponseGenerator>();
            return service;
        }
        public static IServiceCollection AddDefaultService(this IServiceCollection services)
        {
            services.AddTransient<IClaimsService, DefaultClaimsService>();
            services.AddTransient<ITokenService, TokenService>();
            services.TryAddSingleton<ISystemClock, SystemClock>();
            return services;
        }
        public static IServiceCollection AddDefaultSecretParsers(this IServiceCollection service)
        {
            service.AddTransient<ISecretParser, PostBodySecretParser>();
            return service;
        }
    }
}

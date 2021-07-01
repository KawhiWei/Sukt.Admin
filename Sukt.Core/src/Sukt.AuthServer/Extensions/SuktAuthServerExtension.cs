using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Sukt.AuthServer.Constants;
using Sukt.AuthServer.Domain.SuktAuthServer.SuktApplicationStore;
using Sukt.AuthServer.EndpointHandler;
using Sukt.AuthServer.EndpointRouterHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Extensions
{
    /// <summary>
    /// 默认处理器注册到DI容器内
    /// </summary>
    public static class SuktAuthServerExtension
    {
        /// <summary>
        /// 默认断点路由器注册
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static IServiceCollection AddDefaultEndpoints(this IServiceCollection service)
        {
            service.AddTransient<IEndpointRouter, EndpointRouter>();
            service.AddEndPoint<AuthorizeEndpoint>(EndpointNames.Authorize, ProtocolRoutePaths.Authorize.EnsureLeadingSlash());
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
    }
}

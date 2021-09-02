using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Sukt.AuthServer.EndpointHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.EndpointRouterHandler
{
    public class EndpointRouter : IEndpointRouter
    {
        private readonly IEnumerable<Endpoint> endpoints;
        private readonly ILogger _logger;

        public EndpointRouter(IEnumerable<Endpoint> endpoints, ILogger<EndpointRouter> logger)
        {
            this.endpoints = endpoints;
            _logger = logger;
        }
        /// <summary>
        /// 根据Http请求获取处理上下文
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public IEndpointHandler FindHandler(HttpContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            foreach (var endpoint in endpoints)
            {
                var handlerpath = endpoint.Path;
                if (context.Request.Path.Equals(handlerpath, StringComparison.OrdinalIgnoreCase))
                {
                    var endpointName = endpoint.Name;
                    _logger.LogDebug("请求路由地址与端点路由匹配成功！", context.Request.Path, endpointName);
                    return GetEndpointHandler(endpoint, context);
                }
            }
            _logger.LogDebug("未找到请求路由地址与端点路由的匹配！", context.Request.Path);
            return null;
        }
        /// <summary>
        /// 获取端点路由处理器
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private IEndpointHandler GetEndpointHandler(Endpoint endpoint, HttpContext context)
        {
            //获取处理器
            if (context.RequestServices.GetService(endpoint.Handler) is IEndpointHandler handler)
            {
                _logger.LogDebug("处理器获取成功！", endpoint.Name, endpoint.Handler.FullName);
                return handler;
            }
            _logger.LogDebug("处理器获取失败！", endpoint.Name, endpoint.Handler.FullName);
            return null;
        }
    }
}

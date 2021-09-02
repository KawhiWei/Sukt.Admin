using Microsoft.AspNetCore.Http;
using Sukt.AuthServer.EndpointHandler;
using System;

namespace Sukt.AuthServer
{
    /// <summary>
    /// 从请求上下文中根据路由地址获取处理器实现
    /// </summary>
    public interface IEndpointRouter
    {
        IEndpointHandler FindHandler(HttpContext context);
    }
}

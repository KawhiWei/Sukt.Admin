using Microsoft.AspNetCore.Http;
using Sukt.AuthServer.EndpointHandler.EndpointHandlerResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.EndpointHandler
{
    /// <summary>
    /// 认证处理器抽象基类
    /// </summary>
    internal abstract class AuthorizeEndpointBase : IEndpointHandler
    {
        public abstract Task<IEndpointResult> HandlerProcessAsync(HttpContext context);
    }
}

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
    /// 客户端认证处理基类
    /// </summary>
    internal class AuthorizeEndpoint : AuthorizeEndpointBase
    {
        /// <summary>
        /// 重写抽象类
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<IEndpointResult> HandlerProcessAsync(HttpContext context)
        {
            await Task.CompletedTask;
            return new AuthorizeResult();
        }
    }
}

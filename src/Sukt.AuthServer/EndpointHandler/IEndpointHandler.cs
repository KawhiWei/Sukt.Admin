using Microsoft.AspNetCore.Http;
using Sukt.AuthServer.EndpointHandler.EndpointHandlerResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.EndpointHandler
{
    public interface IEndpointHandler
    {
        /// <summary>
        /// handler 处理器接口定义
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task<IEndpointResult> HandlerProcessAsync(HttpContext context);
    }
}

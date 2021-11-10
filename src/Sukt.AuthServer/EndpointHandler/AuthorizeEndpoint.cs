using Microsoft.AspNetCore.Http;
using Sukt.AuthServer.EndpointHandler.EndpointHandlerResult;
using Sukt.AuthServer.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
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
            NameValueCollection values;
            //判断是否是get请求 如果是get请求拿出后面的请求参数
            if (HttpMethods.IsGet(context.Request.Method))
            {
                values = context.Request.Query.AsNameValueCollection();
            }
            else if (HttpMethods.IsPost(context.Request.Method))
            {
                if (!context.Request.HasApplicationFormContentType())
                {
                    return new StatusCodeResult(HttpStatusCode.UnsupportedMediaType);
                }

                values = context.Request.Form.AsNameValueCollection();
            }
            else
            {
                return new StatusCodeResult(HttpStatusCode.MethodNotAllowed);
            }


            await Task.CompletedTask;
            return new AuthorizeResult();
        }
    }
}

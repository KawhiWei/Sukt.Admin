using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UwlAPI.Tools.MiddleWare.ExceptionMiddleWare;

namespace UwlAPI.Tools.Extensions
{
    /// <summary>
    /// 异常日志记录中间件扩展
    /// </summary>
    public static class LogMiddlewareExtensions
    {
        /// <summary>
        /// 扩展注入异常记录中间件
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseLog(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionLogMiddleware>();
        }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Extensions
{
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// 判断是否是模拟表单提交
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        internal static bool HasApplicationFormContentType(this HttpRequest request)
        {
            if (request.ContentType is null) return false;

            if (MediaTypeHeaderValue.TryParse(request.ContentType, out var header))
            {
                // Content-Type: application/x-www-form-urlencoded; charset=utf-8
                return header.MediaType.Equals("application/x-www-form-urlencoded", StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }
    }
}

using Microsoft.AspNetCore.Http;
using Sukt.Core.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.AspNetCore.Extensions
{
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// 判断是否为json格式
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool IsJsonContextType(this HttpRequest request)
        {

            request.NotNull(nameof(request));
            return request.Headers?["Content-Type"].ToString()?.IndexOf("application/json", StringComparison.OrdinalIgnoreCase) > -1;
        }
        /// <summary>
        /// 判断是否AJAX请求
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            request.NotNull(nameof(request));
            var flag = request.Headers?["X-Requested-With"].ToString()?.Equals("XMLHttpRequest", StringComparison.OrdinalIgnoreCase);
            return flag.HasValue && flag.Value;
        }
    }
}

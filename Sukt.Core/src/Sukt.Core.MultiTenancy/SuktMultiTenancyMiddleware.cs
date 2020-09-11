using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.Shared.SuktDependencyAppModule;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.Core.MultiTenancy
{
    public class SuktMultiTenancyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly DictionaryAccessor _dictionaryAccessor;
        public SuktMultiTenancyMiddleware(RequestDelegate next, DictionaryAccessor dictionaryAccessor)
        {
            _next = next;
            _dictionaryAccessor = dictionaryAccessor;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var tenantInfo = context.RequestServices.GetRequiredService<TenantInfo>();
            var tenant= context.Request.Headers["Tenant"];
            tenantInfo.Name = "2d3as2d13a";
            //_dictionaryAccessor.GetOrAdd("Tenant", "1213143513");
            await _next(context);
        }
    }
    public static class SuktMultiTenancyMiddlewareExtensions
    {
        /// <summary>
        /// 异常中间件
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>

        public static IApplicationBuilder UseMultiTenancy(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SuktMultiTenancyMiddleware>();
        }
    }
}

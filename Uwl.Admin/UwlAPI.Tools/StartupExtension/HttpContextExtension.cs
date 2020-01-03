using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uwl.Common.HttpContextUser;

namespace UwlAPI.Tools.StartupExtension
{
    /// <summary>
    /// 启动服务
    /// </summary>
    public static class HttpContextExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public static void AddHttpContextExtension(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUsers, HttpContextUserServer>();
        }
    }
}

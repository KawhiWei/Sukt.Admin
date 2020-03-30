using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Shared.SuktAppModules
{
    /// <summary>
    /// start服务模块基类
    /// </summary>
    public abstract class SuktAppModuleBase
    {
        /// <summary>
        /// 将模块服务添加到依赖注入服务容器中
        /// </summary>
        /// <param name="service">依赖注入服务容器</param>
        /// <returns></returns>
        public virtual IServiceCollection ConfigureServices(IServiceCollection service)
        {
            return service;
        }
        /// <summary>
        /// Http管道方法由运行时调用;使用此方法配置HTTP请求管道。
        /// </summary>
        public virtual void Configure(IApplicationBuilder applicationBuilder)
        {

        }
    }
}

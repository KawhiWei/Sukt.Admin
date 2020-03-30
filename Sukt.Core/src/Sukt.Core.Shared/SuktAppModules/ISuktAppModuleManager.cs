using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Shared.SuktAppModules
{
    /// <summary>
    /// SuktAppModule管理接口定义
    /// </summary>
    public interface ISuktAppModuleManager
    {
        /// <summary>
        /// 加载服务集合
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        IServiceCollection LoadModules(IServiceCollection services);

        List<SuktAppModuleBase> SuktSourceModules { get; }

        //此方法由运行时调用。使用此方法配置HTTP请求管道。
        void Configure(IApplicationBuilder applicationBuilder);
    }
}

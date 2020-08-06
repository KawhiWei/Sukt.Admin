using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Shared.Modules
{
    /// <summary>
    /// 定义模块加载接口
    /// </summary>
    public interface ISuktAppModule:IApplicationInitialization
    {
        void ConfigureServices(ConfigureServicesContext context);
        /// <summary>
        /// 服务依赖集合
        /// </summary>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        Type[] GetDependedTypes(Type moduleType = null);
    }
}

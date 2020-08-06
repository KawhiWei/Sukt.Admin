using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Shared.Modules
{
    /// <summary>
    /// 
    /// </summary>
    public interface IStartupModuleRunner : IModuleApplication
    {
        void ConfigureServices(IServiceCollection services);
        void Initialize(IServiceProvider service);
    }
}

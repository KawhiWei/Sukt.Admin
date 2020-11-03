using Microsoft.Extensions.DependencyInjection;
using System;

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
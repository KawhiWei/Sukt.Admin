using Microsoft.Extensions.DependencyInjection;

namespace Sukt.Core.Shared.Modules
{
    /// <summary>
    /// 自定义配置服务上下文
    /// </summary>
    public class ConfigureServicesContext
    {
        public ConfigureServicesContext(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }
    }
}
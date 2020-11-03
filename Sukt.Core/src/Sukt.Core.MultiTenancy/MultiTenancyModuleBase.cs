using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.Shared.Modules;

namespace Sukt.Core.MultiTenancy
{
    public abstract class MultiTenancyModuleBase : SuktAppModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.AddScoped<TenantInfo>();
        }
    }
}
using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.Shared;

namespace Sukt.Core.AuthenticationCenter.Startups
{
    public class AuthenticationCenterEntityFrameworkCoreModule : EntityFrameworkCoreModule
    {
        protected override IServiceCollection AddAddSuktDbContextWnitUnitOfWork(IServiceCollection services)
        {
            services.AddSuktDbContext<IdentityServer4DefaultDbContext>();
            services.AddUnitOfWork<IdentityServer4DefaultDbContext>();
            return services;
        }
    }
}

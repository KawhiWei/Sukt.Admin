using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.Shared.Entity;
using Sukt.Core.Shared.Modules;

namespace Sukt.Core.Shared
{
    public class EntityFrameworkCoreModule : SuktAppModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            AddAddSuktDbContextWnitUnitOfWork(context.Services);
            AddRepository(context.Services);
        }
        protected virtual IServiceCollection AddRepository(IServiceCollection services)
        {
            services.AddScoped(typeof(IEFCoreRepository<,>), typeof(BaseRepository<,>));
            return services;
        }

        protected virtual IServiceCollection AddAddSuktDbContextWnitUnitOfWork(IServiceCollection services)
        {
            services.AddSuktDbContext<DefaultDbContext>();
            services.AddUnitOfWork<DefaultDbContext>();
            return services;
        }
    }
}

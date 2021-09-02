using Microsoft.Extensions.DependencyInjection;
using SuktCore.Shared;
using SuktCore.Shared.Entity;
using SuktCore.Shared.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Domain.Repository
{
    public class EntityFrameworkCoreModule: SuktAppModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {

            context.Services.AddSuktDbContext<EntityFrameworkCoreContext>();
            context.Services.AddUnitOfWork<EntityFrameworkCoreContext>();
            context.Services.AddRepository();
            //AddAddSuktDbContextWnitUnitOfWork(context.Services);
            //AddRepository(context.Services);
        }
        //protected virtual IServiceCollection AddRepository(IServiceCollection services)
        //{
        //    services.AddScoped(typeof(IEFCoreRepository<,>), typeof(BaseRepository<,>));
        //    services.AddScoped(typeof(IAggregateRootRepository<,>), typeof(AggregateRootBaseRepository<,>));
        //    return services;
        //}

        //protected virtual IServiceCollection AddAddSuktDbContextWnitUnitOfWork(IServiceCollection services)
        //{
        //    services.AddSuktDbContext<DefaultDbContext>();
        //    services.AddUnitOfWork<DefaultDbContext>();
        //    return services;
        //}
    }
}

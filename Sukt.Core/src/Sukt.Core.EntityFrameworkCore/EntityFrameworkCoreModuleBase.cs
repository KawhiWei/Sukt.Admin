using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.Shared.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Shared
{
    public abstract class EntityFrameworkCoreModuleBase: SuktAppModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            UseSql(context.Services);
            AddUnitOfWork(context.Services); ;
            AddRepository(context.Services);

            //return base.ConfigureServices(service);
        }
        protected abstract IServiceCollection AddUnitOfWork(IServiceCollection services);


        protected abstract IServiceCollection AddRepository(IServiceCollection services);



        protected abstract IServiceCollection UseSql(IServiceCollection services);
    }
}

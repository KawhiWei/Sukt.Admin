using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.Shared.SuktAppModules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.EntityFrameworkCore
{
    public abstract class EntityFrameworkCoreModuleBase: SuktAppModuleBase
    {
        public override IServiceCollection ConfigureServices(IServiceCollection service)
        {
            service = UseSql(service);
            service = AddUnitOfWork(service); ;
            service = AddRepository(service);

            return base.ConfigureServices(service);
        }
        protected abstract IServiceCollection AddUnitOfWork(IServiceCollection services);


        protected abstract IServiceCollection AddRepository(IServiceCollection services);



        protected abstract IServiceCollection UseSql(IServiceCollection services);
    }
}

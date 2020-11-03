using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Sukt.Core.MongoDB.Repositorys;
using Sukt.Core.Shared.Modules;

namespace Sukt.Core.MongoDB
{
    public abstract class MongoDBModuleBase : SuktAppModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            AddDbContext(context.Services);
            AddRepository(context.Services);
        }

        public virtual void AddRepository(IServiceCollection services)
        {
            services.TryAddScoped(typeof(IMongoDBRepository<,>), typeof(MongoDBRepository<,>));
        }

        protected abstract void AddDbContext(IServiceCollection services);
    }
}
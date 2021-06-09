using Microsoft.Extensions.DependencyInjection;
using Sukt.EntityFrameworkCore;
using Sukt.Module.Core;
using Sukt.Module.Core.AppOption;
using Sukt.Module.Core.Extensions;
using Sukt.Module.Core.Modules;
using System.Linq;

namespace Sukt.Core.EntityFrameworkCore
{
    public class AuthenticationCenterEntityFrameworkCoreModule : EntityFrameworkCoreBaseModule
    {
        public override void AddDbContextWithUnitOfWork(IServiceCollection services)
        {
            var settings = services.GetAppSettings();
            var configuration = services.GetConfiguration();
            services.Configure<AppOptionSettings>(configuration.GetSection("SuktCore"));
            services.AddSuktDbContext<SuktContext>(x => {
                x.ConnectionString = settings.DbContexts.Values.First().ConnectionString;
                x.DatabaseType = settings.DbContexts.Values.First().DatabaseType;
                x.MigrationsAssemblyName = settings.DbContexts.Values.First().MigrationsAssemblyName;
            });
            services.AddUnitOfWork<SuktContext>();
        }
    }
}

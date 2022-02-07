namespace Sukt.Admin.EntityFrameworkCore
{
    public class AdminEntityFrameworkCoreModule : EntityFrameworkCoreBaseModule
    {
        public override void AddDbContextWithUnitOfWork(IServiceCollection services)
        {
            var settings = services.GetAppSettings();
            var configuration = services.GetConfiguration();
            services.Configure<AppOptionSettings>(configuration.GetSection("SuktCore"));
            services.AddSuktDbContext<SuktAdminContext>(x =>
            {
                x.ConnectionString = settings.DbContexts.Values.First().ConnectionString;
                x.DatabaseType = settings.DbContexts.Values.First().DatabaseType;
                x.MigrationsAssemblyName = typeof(SuktAdminContext).Assembly.GetName().Name;// settings.DbContexts.Values.First().MigrationsAssemblyName;
            });
            services.AddUnitOfWork<SuktAdminContext>();
        }
    }
}

using Microsoft.Extensions.Configuration;
using Sukt.Module.Core.Modules;
using Sukt.Module.Core.SeedDatas;
using Sukt.Module.Core.UnitOfWorks;

namespace Sukt.Admin.EntityFrameworkCore
{
    public class MigrationModule : SuktAppModule
    {
        public override void ApplicationInitialization(ApplicationContext context)
        {
            var app = context.GetApplicationBuilder();
            var configuration = context.ServiceProvider.GetService<IConfiguration>();
            var isAutoMigration = configuration["SuktCore:Migrations:IsAutoMigration"].AsTo<bool>();
            if (isAutoMigration)
            {
                context.ServiceProvider.CreateScoped(provider =>
                {
                    var unitOfWork = provider.GetService<IUnitOfWork>();
#pragma warning disable CS8602 // 解引用可能出现空引用。
                    var dbContext = unitOfWork.GetDbContext();
#pragma warning restore CS8602 // 解引用可能出现空引用。
#if DEBUG
                    dbContext.Database.EnsureCreated();
#else
                    string[] migrations = dbContext.Database.GetPendingMigrations().ToArray();
                    if (migrations.Length > 0)
                    {
                        dbContext.Database.Migrate();
                    }
#endif
                });
            }
            var isAddSeedData = configuration["SuktCore:Migrations:IsAddSeedData"].AsTo<bool>();
            if (isAddSeedData)
            {
                var seedDatas = context.ServiceProvider.GetServices<ISeedData>();

#pragma warning disable CS8602 // 解引用可能出现空引用。
                foreach (var seed in seedDatas?.OrderBy(o => o.Order).Where(o => !o.Disable))
#pragma warning restore CS8602 // 解引用可能出现空引用。
                {
                    seed.Initialize();
                }
            }
        }
    }
}
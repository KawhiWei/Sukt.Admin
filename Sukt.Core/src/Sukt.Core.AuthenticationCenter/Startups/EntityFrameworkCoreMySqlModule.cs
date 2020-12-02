using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.MultiTenancy;
using Sukt.Core.Shared;
using Sukt.Core.Shared.Entity;
using Sukt.Core.Shared.Extensions;

namespace Sukt.Core.AuthenticationCenter.Startups
{
    public class EntityFrameworkCoreMySqlModule : EntityFrameworkCoreModuleBase
    {
        /// <summary>
        /// 添加仓储
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        protected override IServiceCollection AddRepository(IServiceCollection services)
        {
            services.AddScoped(typeof(IEFCoreRepository<,>), typeof(BaseRepository<,>));
            return services;
        }

        /// <summary>
        /// 添加工作单元
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        protected override IServiceCollection AddUnitOfWork(IServiceCollection services)
        {
            return services.AddScoped<IUnitOfWork, UnitOfWork<DefaultDbContext>>();
        }

        /// <summary>
        /// 重写方法
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        protected override IServiceCollection UseSql(IServiceCollection services)
        {
            var mysqlconn = services.GetFileByConfiguration("SuktCore:DbContext:MysqlConnectionString", "未找到存放MySql数据库链接的文件");
            services.AddDbContext<DefaultDbContext>((serviceProvider, options) =>
            {
                var resolver = serviceProvider.GetRequiredService<ISuktConnectionStringResolver>();
                //var ss = resolver.Resolve();
                options.UseMySql(mysqlconn, assembly => { assembly.MigrationsAssembly("Sukt.Core.Domain.Models"); });
            });
            return services;
        }
    }
}

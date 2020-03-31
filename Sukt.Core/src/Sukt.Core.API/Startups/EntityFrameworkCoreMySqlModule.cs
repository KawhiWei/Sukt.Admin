using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.Domain.ISuktRepository;
using Sukt.Core.Domain.Unitofwork;
using Sukt.Core.EntityFrameworkCore;
using Sukt.Core.EntityFrameworkCore.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sukt.Core.API.Startups
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
            return services.AddScoped(typeof(IEFCoreRepository<,>));
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
            return services;
        }
    }
}

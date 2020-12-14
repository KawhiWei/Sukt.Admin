using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.Shared.Entity;

namespace Sukt.Core.Shared
{
    public static class UnitOfWorkExtensions
    {
        /// <summary>
        /// 添加工作单元
        /// </summary>
        /// <typeparam name="TIUnitOfWork"></typeparam>
        /// <typeparam name="UnitOfWork"></typeparam>
        /// <param name="services"></param>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public static IServiceCollection AddUnitOfWork<TDbContext>(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Scoped)
              where TDbContext : SuktDbContextBase
        {
            ServiceDescriptor serviceDescriptor = new ServiceDescriptor(typeof(IUnitOfWork), typeof(UnitOfWork<TDbContext>), lifetime);
            services.Add(serviceDescriptor);
            return services;
        }
    }
}

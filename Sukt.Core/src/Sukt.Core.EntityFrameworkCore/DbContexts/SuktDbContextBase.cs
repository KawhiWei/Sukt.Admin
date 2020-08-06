using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Sukt.Core.Shared.SuktReflection;
using Sukt.Core.Shared.Extensions;
using Sukt.Core.EntityFrameworkCore;
using Sukt.Core.Shared.Entity;

namespace Sukt.Core.Shared
{
    /// <summary>
    /// 上下文基类
    /// </summary>
    public class SuktDbContextBase : DbContext
    {
        private readonly IServiceProvider _serviceProvider = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options"></param>
        /// <param name="serviceProvider"></param>
        protected SuktDbContextBase(DbContextOptions options, IServiceProvider serviceProvider) : base(options)
        {
            _serviceProvider = serviceProvider;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var typeFinder = _serviceProvider.GetService<ITypeFinder>();
            IEntityMappingConfiguration[] entitymapping = typeFinder.Find(x => x.IsDeriveClassFrom<IEntityMappingConfiguration>()).Select(x => Activator.CreateInstance(x) as IEntityMappingConfiguration).ToArray();
            foreach (var item in entitymapping)
            {
                item.Map(modelBuilder);
            }
        }
        public IUnitOfWork unitOfWork { get; set; }
        /// <summary>
        /// 异步保存
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            return base.SaveChangesAsync(cancellationToken);
        }
        /// <summary>
        /// 保存更改
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}

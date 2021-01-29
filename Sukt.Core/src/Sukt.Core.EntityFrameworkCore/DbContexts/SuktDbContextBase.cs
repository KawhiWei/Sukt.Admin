using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sukt.Core.Shared.AppOption;
using Sukt.Core.Shared.Audit;
using Sukt.Core.Shared.Entity;
using Sukt.Core.Shared.Extensions;
using Sukt.Core.Shared.SuktDependencyAppModule;
using Sukt.Core.Shared.SuktReflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace Sukt.Core.Shared
{
    /// <summary>
    /// 上下文基类
    /// </summary>
    public class SuktDbContextBase : DbContext
    {
        protected readonly IServiceProvider _serviceProvider = null;
        protected readonly AppOptionSettings _appOptionSettings;
        private readonly IGetChangeTracker _changeTracker;
        protected readonly ILogger _logger = null;
        protected readonly AuditEntryDictionaryScoped _auditEntryDictionaryScoped;
        private readonly IPrincipal _principal;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options"></param>
        /// <param name="serviceProvider"></param>
        protected SuktDbContextBase(DbContextOptions options, IServiceProvider serviceProvider) : base(options)
        {
            _serviceProvider = serviceProvider;
            _appOptionSettings = serviceProvider.GetAppSettings();
            this._logger = serviceProvider.GetLogger(GetType());
            _auditEntryDictionaryScoped = serviceProvider.GetService<AuditEntryDictionaryScoped>();
            _changeTracker = _serviceProvider.GetService<IGetChangeTracker>();
            _principal = serviceProvider.GetService<IPrincipal>();
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
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyConcepts();
            var result = OnBeforeSaveChanges();
            var count = await base.SaveChangesAsync(cancellationToken);
            OnCompleted(count, result);
            return count;
        }

        /// <summary>
        /// 保存更改
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            ApplyConcepts();
            var result = OnBeforeSaveChanges();
            var count = base.SaveChanges();
            OnCompleted(count, result);
            return count;
        }
        protected virtual void OnCompleted(int count, object sender)
        {
            if (_appOptionSettings.AuditEnabled)
            {
                if (count > 0 && sender != null && sender is List<AuditEntryInputDto> senders)
                {
                    var auditChange = _auditEntryDictionaryScoped.AuditChange;
                    if (auditChange != null)
                    {
                        auditChange.AuditEntryInputDtos.AddRange(senders);
                    }
                }
            }
            _logger.LogInformation($"进入保存更新成功方法");
        }
        protected virtual object OnBeforeSaveChanges()
        {
            _logger.LogInformation($"进入开始保存更改方法");
            return GetAuditEntitys();
        }
        /// <summary>
        /// 获取修改过的实体数据（修改前/修改后）进行数据审计
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerable<AuditEntryInputDto> GetAuditEntitys()
        {
            return _changeTracker.GetChangeTrackerList(FindChangedEntries());
        }
        /// <summary>
        /// 获取实体跟踪状态
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerable<EntityEntry> FindChangedEntries()
        {
            return this.ChangeTracker.Entries().Where(x => x.State == EntityState.Added || x.State == EntityState.Deleted || x.State == EntityState.Modified).ToList();
        }
        /// <summary>
        /// 在上下文内写入创建人创建时间等等
        /// </summary>
        protected virtual void ApplyConcepts()
        {
            var entries = this.FindChangedEntries().ToList();
            foreach (var entity in entries)
            {
                if (entity.Entity is ICreatedAudited<Guid> createdTime && entity.State == EntityState.Added)
                {
                    createdTime.CreatedAt = DateTime.Now;
                    if (_principal != null && _principal.Identity != null)
                        createdTime.CreatedId = _principal.Identity.GetUesrId<Guid>();
                }
                if (entity.Entity is IModifyAudited<Guid> ModificationAuditedUserId && entity.State == EntityState.Modified)
                {
                    ModificationAuditedUserId.LastModifedAt = DateTime.Now;
                    if (_principal != null && _principal.Identity != null)
                        ModificationAuditedUserId.LastModifyId = _principal.Identity.GetUesrId<Guid>();
                }
            }
        }
    }
}

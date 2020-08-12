using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sukt.Core.Shared.Entity;
using Sukt.Core.Shared.Events.EventBus;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sukt.Core.Shared.Audit;

namespace Sukt.Core.Shared
{
    public class DefaultDbContext : SuktDbContextBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IEventBus _bus;
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options, IServiceProvider serviceProvider)
          : base(options, serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _bus = _serviceProvider.GetService<IEventBus>();
        }
        protected List<AuditEntry> AuditEntries { get; set; }
        /// <summary>
        /// 发布消息事件到日志处理事件
        /// </summary>
        /// <returns></returns>
        protected override async Task AfterSaveChanges()
        {
            //var model = new AuditEntry();
            //model.NewValues = new Dictionary<string, object>();
            //model.OriginalValues = new Dictionary<string, object>();
            //foreach (var entityEntry in this.ChangeTracker.Entries())
            //{
            //    var properties = entityEntry.Metadata.GetProperties();
            //    foreach (var propertie in properties)
            //    {
            //        var propertyEntry = entityEntry.Property(propertie.Name);//获取字段名
            //        switch (entityEntry.State)
            //        {
            //            case EntityState.Detached:
            //                model.OperationType = DataOperationType.None;
            //                model.NewValues.Add(propertie.Name, propertyEntry.CurrentValue?.ToString());//当前值
            //                model.OriginalValues.Add(propertie.Name, propertyEntry.CurrentValue?.ToString());//当前值
            //                break;
            //            case EntityState.Unchanged:
            //                model.OperationType = DataOperationType.Add;
            //                model.NewValues.Add(propertie.Name, propertyEntry.CurrentValue?.ToString());//当前值
            //                model.OriginalValues.Add(propertie.Name, propertyEntry.CurrentValue?.ToString());//当前值
            //                break;
            //            case EntityState.Deleted:
            //                model.OperationType = DataOperationType.Delete;
            //                model.NewValues.Add(propertie.Name, propertyEntry.CurrentValue?.ToString());//当前值
            //                model.OriginalValues.Add(propertie.Name, propertyEntry.CurrentValue?.ToString());//当前值
            //                break;
            //            case EntityState.Modified:
            //                model.OperationType = DataOperationType.Update;
            //                model.NewValues.Add(propertie.Name, propertyEntry.CurrentValue?.ToString());//当前值
            //                model.OriginalValues.Add(propertie.Name, propertyEntry.CurrentValue?.ToString());//当前值
            //                break;
            //            case EntityState.Added:
            //                model.OperationType = DataOperationType.Add;
            //                model.NewValues.Add(propertie.Name, propertyEntry.CurrentValue?.ToString());//当前值
            //                model.OriginalValues.Add(propertie.Name, propertyEntry.CurrentValue?.ToString());//当前值
            //                break;
            //        }
            //    }
            //}
            await _bus.PublishAsync(new AuditEvent() { Entries = this.ChangeTracker.Entries() });
        }
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result =  await base.SaveChangesAsync(cancellationToken);
            await this.AfterSaveChanges();
            return result;
        }
    }
}

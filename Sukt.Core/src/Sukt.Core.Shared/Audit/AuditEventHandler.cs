using Microsoft.EntityFrameworkCore;
using Sukt.Core.Shared.Entity;
using Sukt.Core.Shared.Events;
using Sukt.Core.Shared.SuktDependencyAppModule;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sukt.Core.Shared.Audit
{
    /// <summary>
    /// 操作日志事件处理器
    /// </summary>
    public class AuditEventHandler : NotificationHandlerBase<AuditEvent>
    {
        private IServiceProvider _serviceProvider = null;
        private DictionaryAccessor _dictionaryAccessor;
        public AuditEventHandler(IServiceProvider serviceProvider, DictionaryAccessor dictionaryAccessor)
        {
            _serviceProvider = serviceProvider;
            _dictionaryAccessor = dictionaryAccessor;
        }

        public override Task Handle(AuditEvent @event, CancellationToken cancellationToken)
        {
            var auditlist = new List<AuditEntry>();
            foreach (var entityEntry in @event.Entries)
            {
                var auditmodel = new AuditEntry();
                auditmodel.LastModifedAt = DateTime.Now;
                auditmodel.CreatedAt = DateTime.Now;
                auditmodel.NewValues = new Dictionary<string, object>();
                auditmodel.OriginalValues = new Dictionary<string, object>();
                var properties = entityEntry.Metadata.GetProperties();
                foreach (var propertie in properties)
                {
                    var propertyEntry = entityEntry.Property(propertie.Name);//获取字段名
                    switch (entityEntry.State)
                    {
                        case EntityState.Detached:
                            auditmodel.OperationType = DataOperationType.None;
                            auditmodel.NewValues.Add(propertie.Name, propertyEntry.CurrentValue?.ToString());//当前值
                            auditmodel.OriginalValues.Add(propertie.Name, propertyEntry.CurrentValue?.ToString());//当前值
                            break;
                        case EntityState.Unchanged:
                            auditmodel.OperationType = DataOperationType.Add;
                            auditmodel.NewValues.Add(propertie.Name,propertyEntry.CurrentValue?.ToString());//当前值
                            auditmodel.OriginalValues.Add(propertie.Name, propertyEntry.CurrentValue?.ToString());//当前值
                            break;
                        case EntityState.Deleted:
                            auditmodel.OperationType = DataOperationType.Delete;
                            auditmodel.NewValues.Add(propertie.Name, propertyEntry.CurrentValue?.ToString());//当前值
                            auditmodel.OriginalValues.Add(propertie.Name, propertyEntry.CurrentValue?.ToString());//当前值
                            break;
                        case EntityState.Modified:
                            auditmodel.OperationType = DataOperationType.Update;
                            auditmodel.NewValues.Add(propertie.Name, propertyEntry.CurrentValue?.ToString());//当前值
                            auditmodel.OriginalValues.Add(propertie.Name, propertyEntry.CurrentValue?.ToString());//当前值
                            break;
                        case EntityState.Added:
                            auditmodel.OperationType = DataOperationType.Add;
                            auditmodel.NewValues.Add(propertie.Name, propertyEntry.CurrentValue?.ToString());//当前值
                            auditmodel.OriginalValues.Add(propertie.Name, propertyEntry.CurrentValue?.ToString());//当前值
                            break;
                    }
                }
                auditlist.Add(auditmodel);
            }
            Console.WriteLine($"事件信息：{@event}");
            _dictionaryAccessor.GetOrAdd("audit", auditlist);
            return Task.CompletedTask;
        }
    }
}

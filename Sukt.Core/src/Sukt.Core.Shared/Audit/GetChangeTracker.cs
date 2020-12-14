using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sukt.Core.Shared.Extensions;
using System.Collections.Generic;

namespace Sukt.Core.Shared.Audit
{
    /// <summary>
    /// 获取实体状态记录审计日志接口实现
    /// </summary>
    public class GetChangeTracker : IGetChangeTracker
    {
        /// <summary>
        /// 获取实体状态记录审计日志
        /// </summary>
        /// <param name="Entries"></param>
        /// <returns></returns>
        public List<AuditEntryInputDto> GetChangeTrackerList(IEnumerable<EntityEntry> Entries)
        {
            var list = new List<AuditEntryInputDto>();
            foreach (var entityEntry in Entries)
            {
                var auditentry = new AuditEntryInputDto();
                auditentry.TableName = entityEntry.Metadata.GetTableName();
                auditentry.EntityAllName = entityEntry.Metadata.Name;
                auditentry.EntityDisplayName = entityEntry.Entity.GetType().ToDescription();
                //auditentry.TableName=
                switch (entityEntry.State)
                {
                    case EntityState.Detached:
                        auditentry.OperationType = DataOperationType.None;
                        break;

                    case EntityState.Unchanged:
                        auditentry.OperationType = DataOperationType.None;
                        break;

                    case EntityState.Deleted:
                        auditentry.OperationType = DataOperationType.Delete;
                        break;

                    case EntityState.Modified:
                        auditentry.OperationType = DataOperationType.Update;
                        break;

                    case EntityState.Added:
                        auditentry.OperationType = DataOperationType.Add;
                        break;
                }
                var properties = entityEntry.Metadata.GetProperties();
                foreach (var propertie in properties)
                {
                    var AuditPropertys = new AuditPropertysEntryInputDto();
                    var propertyEntry = entityEntry.Property(propertie.Name);//获取字段名
                    if (propertyEntry.Metadata.IsPrimaryKey())
                    {
                        auditentry.KeyValues.Add(propertie.Name, propertyEntry.CurrentValue?.ToString());
                    }
                    else
                    {
                        AuditPropertys.Properties = propertie.Name;
                        AuditPropertys.NewValues = propertyEntry.CurrentValue?.ToString();
                        AuditPropertys.OriginalValues = propertyEntry.OriginalValue?.ToString();
                        AuditPropertys.PropertiesType = propertie.ClrType.FullName;
                        AuditPropertys.PropertieDisplayName = propertyEntry.Metadata.PropertyInfo.ToDescription();
                        auditentry.PropertysEntryInputDto.Add(AuditPropertys);
                    }
                }
                list.Add(auditentry);
            }
            return list;
        }
    }
}

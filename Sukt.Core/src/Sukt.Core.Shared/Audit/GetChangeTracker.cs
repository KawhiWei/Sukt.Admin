using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sukt.Core.Shared.Attributes;
using Sukt.Core.Shared.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
            EntityState[] states = { EntityState.Added, EntityState.Modified, EntityState.Deleted };
            return Entries.Where(x => x.Entity != null && states.Contains(x.State) && x.GetType().IsDefined(typeof(DisableAuditingAttribute)) == false).ToArray().Select(o => this.CreateAuditEntry(o)).ToList();
        }

        private AuditEntryInputDto CreateAuditEntry(EntityEntry entityEntry)
        {
            var entity = entityEntry.Entity;
            var type = entity.GetType();
            var displayName = type.ToDescription(); //得到实体上特性
            DataOperationType changeType = DataOperationType.Add;
            switch (entityEntry.State)
            {
                case EntityState.Deleted:
                    changeType = DataOperationType.Delete;
                    break;

                case EntityState.Modified:
                    changeType = DataOperationType.Update;
                    break;

                case EntityState.Added:
                    changeType = DataOperationType.Add;
                    break;
            }
            AuditEntryInputDto auditEntryInput = new AuditEntryInputDto();
            auditEntryInput.KeyValues = new Dictionary<string, object>();
            auditEntryInput.EntityAllName = type.FullName;
            auditEntryInput.EntityDisplayName = displayName;
            auditEntryInput.OperationType = changeType;
            auditEntryInput.PropertysEntryInputDto = GetAuditPropertys(entityEntry);
            auditEntryInput.KeyValues = new Dictionary<string, object>() {
                { "Id",GetEntityKey(entity)}
            };
            return auditEntryInput;
        }
        /// <summary>
        /// 得到实体主键 
        /// </summary>
        /// <param name="entityAsObj"></param>
        /// <returns></returns>
        private string GetEntityKey(object entityAsObj)
        {
            return entityAsObj
                .GetType().GetProperty("Id")?
                .GetValue(entityAsObj)?
                .ToJson();
        }
        /// <summary>
        /// 得到审计属性
        /// </summary>
        /// <param name="entityEntry"></param>
        /// <returns></returns>
        private List<AuditPropertysEntryInputDto> GetAuditPropertys(EntityEntry entityEntry)
        {
            List<AuditPropertysEntryInputDto> propertyDtos = new List<AuditPropertysEntryInputDto>();

            foreach (var propertie in entityEntry.CurrentValues.Properties.Where(p => !p.IsConcurrencyToken && p.PropertyInfo.GetCustomAttribute<DisableAuditingAttribute>() == null))
            {
                var propertyEntry = entityEntry.Property(propertie.Name);//获取字段名
                AuditPropertysEntryInputDto propertyDto = new AuditPropertysEntryInputDto();
                propertyDto.Properties = propertie.Name;
                propertyDto.PropertieDisplayName = propertyEntry.Metadata.PropertyInfo.ToDescription();
                propertyDto.PropertiesType = propertie.ClrType.FullName;
                if (propertie.IsPrimaryKey())
                {
                    continue;
                }
                if (entityEntry.State == EntityState.Added)
                {
                    var currentValue = propertyEntry.CurrentValue?.ToString();
                    propertyDto.NewValues = currentValue;
                    propertyDtos.Add(propertyDto);
                }
                else if (entityEntry.State == EntityState.Modified)
                {
                    var originalValue = propertyEntry.OriginalValue?.ToString();
                    var currentValue = propertyEntry.CurrentValue?.ToString();
                    if (currentValue != originalValue)
                    {
                        propertyDto.NewValues = currentValue;

                        propertyDto.OriginalValues = originalValue;
                        propertyDtos.Add(propertyDto);
                    }
                }
                else if (entityEntry.State == EntityState.Deleted)
                {

                    var originalValue = propertyEntry.OriginalValue?.ToString();
                    propertyDto.OriginalValues = originalValue;
                    propertyDtos.Add(propertyDto);
                }
            }
            return propertyDtos;
        }
    }
}

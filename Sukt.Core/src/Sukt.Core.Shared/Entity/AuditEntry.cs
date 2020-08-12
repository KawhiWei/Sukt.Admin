using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Sukt.Core.Shared.Entity
{
    public class AuditEntry: EntityBase<Guid>, IFullAuditedEntity<Guid>
    {
        /// <summary>
        /// 执行方法名
        /// </summary>
        [DisplayName("执行方法名")]
        public string Action { get; set; }
        /// <summary>
        /// 功能名称
        /// </summary>
        [DisplayName("功能名称")]
        public string DescriptionName { get; set; }
        /// <summary>
        /// 表明
        /// </summary>
        [DisplayName("表名称")]
        public string TableName { get; set; }
        /// <summary>
        /// 修改前数据
        /// </summary>
        [DisplayName("修改前数据")]
        public Dictionary<string, object> OriginalValues { get; set; }
        /// <summary>
        /// 修改后数据
        /// </summary>
        [DisplayName("修改后数据")]
        public Dictionary<string, object> NewValues { get; set; }
        /// <summary>
        /// 主键
        /// </summary>
        [DisplayName("主键")]
        public Dictionary<string, object> KeyValues { get; set; }
        /// <summary>
        /// 操作类型
        /// </summary>
        [DisplayName("操作类型")]
        public DataOperationType OperationType { get; set; }
        /// <summary>
        /// 属性名称
        /// </summary>
        [DisplayName("属性名称")]
        public Dictionary<string, object> Properties { get; set; }
        /// <summary>
        /// 创建人Id
        /// </summary>
        [DisplayName("创建人Id")]
        public Guid? CreatedId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public virtual DateTime CreatedAt { get; set; }
        /// <summary>
        /// 修改人ID
        /// </summary>
        [DisplayName("修改人ID")]
        public Guid? LastModifyId { get; set; }
        /// <summary>
        ///修改时间
        /// </summary>
        [DisplayName("修改时间")]
        public virtual DateTime LastModifedAt { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        [DisplayName("是否删除")]
        public bool IsDeleted { get; set; }
    }
    public enum DataOperationType : sbyte
    {
        None,
        Add,
        Delete,
        Update
    }
    //public sealed class InternalAuditEntry : AuditEntry
    //{
    //    public List<PropertyEntry> TemporaryProperties { get; set; } = new List<PropertyEntry>();

    //    public InternalAuditEntry(EntityEntry entityEntry)
    //    {
    //        TableName = entityEntry.Metadata.GetTableName();
    //        KeyValues = new Dictionary<string, object>(4);
    //        Properties = new Dictionary<string, object>(16);

    //        //if (entityEntry.Properties.Any(x => x.IsTemporary))
    //        //{
    //        //    TemporaryProperties = new List<PropertyEntry>(4);
    //        //}

    //        if (entityEntry.State == EntityState.Added)
    //        {
    //            OperationType = DataOperationType.Add;
    //            NewValues = new Dictionary<string, object>();
    //        }
    //        else if (entityEntry.State == EntityState.Deleted)
    //        {
    //            OperationType = DataOperationType.Delete;
    //            OriginalValues = new Dictionary<string, object>();
    //        }
    //        else if (entityEntry.State == EntityState.Modified)
    //        {
    //            OperationType = DataOperationType.Update;
    //            OriginalValues = new Dictionary<string, object>();
    //            NewValues = new Dictionary<string, object>();
    //        }
    //        foreach (var propertyEntry in entityEntry.Properties)
    //        {
    //            //if (AuditConfig.AuditConfigOptions.PropertyFilters.Any(f => f.Invoke(entityEntry, propertyEntry) == false))
    //            //{
    //            //    continue;
    //            //}
    //            //if (propertyEntry.IsTemporary)
    //            //{
    //                TemporaryProperties.Add(propertyEntry);
    //                //continue;
    //            //}

    //            var columnName = propertyEntry.Metadata.GetColumnName();
    //            if (propertyEntry.Metadata.IsPrimaryKey())
    //            {
    //                KeyValues[columnName] = propertyEntry.CurrentValue;
    //            }
    //            switch (entityEntry.State)
    //            {
    //                case EntityState.Added:
    //                    NewValues[columnName] = propertyEntry.CurrentValue;
    //                    break;

    //                case EntityState.Deleted:
    //                    OriginalValues[columnName] = propertyEntry.OriginalValue;
    //                    break;

    //                case EntityState.Modified:
    //                    //if (propertyEntry.IsModified || AuditConfig.AuditConfigOptions.SaveUnModifiedProperties)
    //                    //if (propertyEntry.IsModified)
    //                    //{
    //                        OriginalValues[columnName] = propertyEntry.OriginalValue;
    //                        //NewValues[columnName] = propertyEntry.CurrentValue;//修改后数据
    //                    //}
    //                    break;
    //            }
    //        }
    //    }
    //}

}

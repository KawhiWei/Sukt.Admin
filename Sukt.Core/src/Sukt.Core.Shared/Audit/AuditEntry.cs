using MongoDB.Bson;
using Sukt.Core.Shared.Entity;
using System.Collections.Generic;
using System.ComponentModel;

namespace Sukt.Core.Shared.Audit
{
    /// <summary>
    /// 审计日志实体表
    /// </summary>
    [MongoDBTable("AuditEntryLog")]
    [DisplayName("审计日志实体")]
    public class AuditEntry : EntityBase<ObjectId>
    {
        public AuditEntry()
        {
            Id = ObjectId.GenerateNewId();
        }

        /// <summary>
        /// 实体名称
        /// </summary>
        [DisplayName("实体名称")]
        public string EntityAllName { get; set; }

        /// <summary>
        /// 功能名称
        /// </summary>
        [DisplayName("实体显示名称")]
        public string EntityDisplayName { get; set; }

        /// <summary>
        /// 表名称
        /// </summary>
        [DisplayName("表名称")]
        public string TableName { get; set; }

        /// <summary>
        /// 主键
        /// </summary>
        [DisplayName("主键")]
        public Dictionary<string, object> KeyValues { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// 操作类型
        /// </summary>
        [DisplayName("操作类型")]
        public DataOperationType OperationType { get; set; }

        /// <summary>
        /// 审计日志主表Id
        /// </summary>
        [DisplayName("审计日志主表Id")]
        public ObjectId AuditLogId { get; set; }

    }

    public enum DataOperationType : sbyte
    {
        None,
        Add,
        Delete,
        Update
    }
}

using Sukt.Core.Shared.Attributes.AutoMapper;
using Sukt.Core.Shared.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Sukt.Core.Shared.Audit
{
    /// <summary>
    /// 审计日志实体输入Dto
    /// </summary>
    [DisplayName("审计日志输入实体Dto")]
    [SuktAutoMapper(typeof(AuditEntry))]
    public class AuditEntryInputDto
    {
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
        public List<AuditPropertysEntryInputDto> PropertysEntryInputDto { get; set; } = new List<AuditPropertysEntryInputDto>();
    }
}

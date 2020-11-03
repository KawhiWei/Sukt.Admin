using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Sukt.Core.Shared.Audit
{
    /// <summary>
    /// 日志实体属性输出Dto
    /// </summary>
    [DisplayName("日志属性输出Dto")]
    public class AuditPropertyEntryOutputDto
    {
        /// <summary>
        /// 属性名称
        /// </summary>
        [DisplayName("属性名称")]
        public string Properties { get; set; }

        /// <summary>
        /// 属性名称
        /// </summary>
        [DisplayName("属性显示名称")]
        public string PropertieDisplayName { get; set; }

        /// <summary>
        /// 修改前数据
        /// </summary>
        [DisplayName("修改前数据")]
        public string OriginalValues { get; set; }

        /// <summary>
        /// 修改后数据
        /// </summary>
        [DisplayName("修改后数据")]
        public string NewValues { get; set; }

        /// <summary>
        /// 属性类型
        /// </summary>
        [DisplayName("属性类型")]
        public string PropertiesType { get; set; }

        /// <summary>
        /// 审计日志实体Id
        /// </summary>
        [DisplayName("审计日志实体Id")]
        public Guid AuditEntryId { get; set; }
    }
}

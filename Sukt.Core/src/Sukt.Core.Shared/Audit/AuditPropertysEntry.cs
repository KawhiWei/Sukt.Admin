using Sukt.Core.Shared.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Sukt.Core.Shared.Audit
{
    /// <summary>
    /// 审计日志属性表
    /// </summary>
    [DisplayName("审计日志属性表")]
    [MongoDBTable("SuktPropertyLog")]
    public class AuditPropertysEntry : EntityBase<Guid>, IFullAuditedEntity<Guid>
    {
        /// <summary>
        /// 属性名称
        /// </summary>
        [DisplayName("属性名称")]
        public string Properties { get; set; }
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
        /// 实体表Id
        /// </summary>
        [DisplayName("实体表Id")]
        public Guid AuditEntryId { get; set; }
        #region 公共字段
        /// <summary>
        /// 创建人
        /// </summary>
        [DisplayName("创建人")]
        public Guid? CreatedId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// 最后修改人
        /// </summary>
        [DisplayName("最后修改人")]
        public Guid? LastModifyId { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        [DisplayName("最后修改时间")]
        public DateTime LastModifedAt { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        [DisplayName("是否删除")]
        public bool IsDeleted { get; set; }
        #endregion
    }
}

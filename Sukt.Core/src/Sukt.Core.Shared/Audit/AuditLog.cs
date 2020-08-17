using Sukt.Core.Shared.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Sukt.Core.Shared.Audit
{
    /// <summary>
    /// 
    /// </summary>
    [MongoDBTable("SuktAuditLog")]
    [DisplayName("审计日志主表")]
    public class AuditLog : EntityBase<Guid>, IFullAuditedEntity<Guid>
    {
        /// <summary>
        /// 浏览器信息
        /// </summary>
        [DisplayName("浏览器信息")]
        public string BrowserInformation { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        [DisplayName("IP地址")]
        public string Ip { get; set; }
        /// <summary>
        /// 功能名称
        /// </summary>
        [DisplayName("功能名称")]
        public string FunctionName { get; set; }
        /// <summary>
        /// 操作Action
        /// </summary>
        [DisplayName("操作Action")]
        public string Action { get; set; }
        /// <summary>
        /// 执行时长
        /// </summary>
        [DisplayName("执行时长")]
        public double ExecutionDuration { get; set; }
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

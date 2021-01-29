using Sukt.Core.Shared.Entity;
using System;
using System.ComponentModel;

namespace Sukt.Core.Domain.Models.Organization
{
    /// <summary>
    /// 
    /// </summary>
    [DisplayName("组织架构人员管理")]
    public class OrganizationUserEntity : EntityBase<Guid>, IFullAuditedEntity<Guid>, ITenantEntity<Guid>
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [DisplayName("用户Id")]
        public Guid UserId { get; set; }
        /// <summary>
        /// 组织架构Id
        /// </summary>
        [DisplayName("组织架构Id")]
        public Guid OrganizationId { get; set; }
        /// <summary>
        /// 组织架构所有父级Id
        /// </summary>
        [DisplayName("组织架构所有父级Id")]
        public Guid OrganizationNumber { get; set; }
        /// <summary>
        /// 职位Id
        /// </summary>
        [DisplayName("职位Id")]
        public Guid PositionId { get; set; }
        /// <summary>
        /// 租户Id
        /// </summary>
        [DisplayName("租户Id")]
        public Guid TenantId { get; set; }
        #region 公共字段
        /// <summary>
        /// 创建人Id
        /// </summary>
        [DisplayName("创建人Id")]
        public Guid CreatedId { get; set; }
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

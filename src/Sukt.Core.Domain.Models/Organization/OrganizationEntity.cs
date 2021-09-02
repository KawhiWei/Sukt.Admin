using Sukt.Module.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Sukt.Core.Domain.Models.Organization
{
    /// <summary>
    /// 组织架构Entity
    /// </summary>
    [DisplayName("组织架构")]
    public class OrganizationEntity : EntityBase<Guid>, IFullAuditedEntity<Guid>, ITenantEntity<Guid>
    {
        /// <summary>
        /// 父级Id
        /// </summary>
        [DisplayName("父级Id")]
        public Guid? ParentId { get; set; }
        /// <summary>
        /// 组织架构名称
        /// </summary>
        [DisplayName("组织架构名称")]
        public string Name { get; set; }
        /// <summary>
        /// 当前部门所有的父级
        /// </summary>
        [DisplayName("当前部门所有的父级")]
        public string ParentNumber { get; set; }
        /// <summary>
        /// 当前部门深度
        /// </summary>
        [DisplayName("当前部门深度")]
        public int Depth { get; set; }
        /// <summary>
        ///获取或设置 描述
        /// </summary>
        [DisplayName("描述")]
        public virtual string Description { get; set; }
        /// <summary>
        /// 主要负责人
        /// </summary>
        [DisplayName("第一负责人")]
        public virtual Guid? FirstLeader { get; set; }
        /// <summary>
        /// 次要负责人
        /// </summary>
        [DisplayName("次要负责人")]
        public virtual Guid? SecondLeader { get; set; }
        /// <summary>
        /// 租户Id
        /// </summary>
        [DisplayName("租户Id")]
        public Guid TenantId { get; set; }
        /// <summary>
        /// 组织架构集合
        /// </summary>
        public ICollection<OrganizationUserEntity> OrganizationItems { get; private set; }
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

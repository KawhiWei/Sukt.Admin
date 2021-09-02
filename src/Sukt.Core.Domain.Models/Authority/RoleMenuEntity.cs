using Sukt.Core.Domain.Models.Menu;
using Sukt.Module.Core.Entity;
using System;
using System.ComponentModel;

namespace Sukt.Core.Domain.Models.Authority
{
    [DisplayName("角色权限管理")]
    public class RoleMenuEntity : EntityBase<Guid>, IFullAuditedEntity<Guid>, ITenantEntity<Guid>
    {
        /// <summary>
        /// 角色
        /// </summary>
        [DisplayName("角色")]
        public RoleEntity Role { get; set; }

        /// <summary>
        /// 菜单
        /// </summary>
        [DisplayName("菜单")]
        public MenuEntity Menu { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        [DisplayName("租户")]
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

        #endregion 公共字段
    }
}

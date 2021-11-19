using Sukt.Core.Identity;
using Sukt.Module.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Sukt.Core.Domain.Models
{
    /// <summary>
    ///
    /// </summary>
    [DisplayName("用户角色")]
    public class UserRoleEntity : UserRoleBase<Guid, Guid>, IFullAuditedEntity<Guid>, ITenantEntity<Guid>
    {
        public UserRoleEntity() : base()
        {
        }
        public UserRoleEntity(Guid userId,Guid roleId):base()
        {
            UserId = userId;
            RoleId = roleId;
        }

        /// <summary>
        /// 角色Id
        /// </summary>
        public Guid RoleId { get; private set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid UserId { get; private set; }
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
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// 修改人ID
        /// </summary>
        [DisplayName("修改人ID")]
        public Guid? LastModifyId { get; set; }

        /// <summary>
        ///修改时间
        /// </summary>
        [DisplayName("修改时间")]
        public DateTimeOffset? LastModifedAt { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [DisplayName("是否删除")]
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 租户ID
        /// </summary>
        [DisplayName("租户")]
        public Guid TenantId { get; set; }
        #endregion 公共字段
    }
}
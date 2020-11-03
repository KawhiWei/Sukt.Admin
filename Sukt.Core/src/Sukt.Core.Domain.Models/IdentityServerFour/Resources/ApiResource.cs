using Sukt.Core.IdentityServerFour;
using Sukt.Core.Shared.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Sukt.Core.Domain.Models.IdentityServerFour
{
    /// <summary>
    /// api资源
    /// </summary>
    [DisplayName("api资源")]
    public class ApiResource : ApiResourceBase, IFullAuditedEntity<Guid>
    {
        #region 公共字段

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

        #endregion 公共字段

        /// <summary>
        /// 密钥
        /// </summary>
        [DisplayName("密钥")]
        public List<ApiResourceSecret> Secrets { get; set; }

        /// <summary>
        /// 授权范围
        /// </summary>
        [DisplayName("授权范围")]
        public List<ApiResourceScope> Scopes { get; set; }

        /// <summary>
        /// 用户声明
        /// </summary>
        [DisplayName("用户声明")]
        public List<ApiResourceClaim> UserClaims { get; set; }

        /// <summary>
        /// 属性
        /// </summary>
        [DisplayName("属性")]
        public List<ApiResourceProperty> Properties { get; set; }
    }
}
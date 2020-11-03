using Sukt.Core.IdentityServerFour;
using Sukt.Core.Shared.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Sukt.Core.Domain.Models.IdentityServerFour
{
    /// <summary>
    /// 客户端实体
    /// </summary>
    [DisplayName("客户端")]
    public class Client : ClientBase, IFullAuditedEntity<Guid>
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

        #region IdentityServer4原始导航属性

        public List<ClientClaim> Claims { get; set; }

        public List<ClientCorsOrigin> AllowedCorsOrigins { get; set; }

        public List<ClientProperty> Properties { get; set; }

        public List<ClientIdPRestriction> IdentityProviderRestrictions { get; set; }

        public List<ClientRedirectUri> RedirectUris { get; set; }

        public List<ClientPostLogoutRedirectUri> PostLogoutRedirectUris { get; set; }

        public List<ClientSecret> ClientSecrets { get; set; }

        public List<ClientScope> AllowedScopes { get; set; }

        public List<ClientGrantType> AllowedGrantTypes { get; set; }

        #endregion IdentityServer4原始导航属性
    }
}
using Sukt.Module.Core.Entity;
using System;
using System.ComponentModel;

namespace Sukt.Core.Domain.Models.IdentityServerFour
{
    /// <summary>
    /// 客户端退出重定向uri
    /// </summary>
    [DisplayName("客户端退出重定向uri")]
    public class ClientPostLogoutRedirectUri : /*ClientPostLogoutRedirectUriBase,*/EntityBase<Guid>, IFullAuditedEntity<Guid>
    {
        public ClientPostLogoutRedirectUri(string postLogoutRedirectUri)
        {
            PostLogoutRedirectUri = postLogoutRedirectUri;
        }

        /// <summary>
        /// 客户端退出重定向uri
        /// </summary>
        [DisplayName("客户端退出重定向uri")]
        public string PostLogoutRedirectUri { get; private set; }
        /// <summary>
        /// 所属客户端
        /// </summary>
        [DisplayName("所属客户端")]
        public Client Client { get; private set; }
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

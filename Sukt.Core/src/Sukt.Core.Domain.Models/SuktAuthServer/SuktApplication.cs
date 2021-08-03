
using Sukt.Module.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.Core.Domain.Models
{
    /// <summary>
    /// 应用实体
    /// </summary>
    [DisplayName("客户端应用")]
    public class SuktApplication : EntityBase<Guid>, IFullAuditedEntity<Guid>
    {
        public SuktApplication(string clientId, string clientSecret, string clientName, string clientGrantType, string postLogoutRedirectUris, string redirectUris, string properties, string description, string clientScopes)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            ClientName = clientName;
            ClientGrantType = clientGrantType;
            PostLogoutRedirectUris = postLogoutRedirectUris;
            RedirectUris = redirectUris;
            Properties = properties;
            Description = description;
            ClientScopes = clientScopes;
        }
        /// <summary>
        /// 客户端唯一Id
        /// </summary>
        [DisplayName("客户端唯一Id")]
        public string ClientId { get; private set; }
        /// <summary>
        /// 客户端密钥
        /// </summary>
        [DisplayName("客户端密钥")]
        public string ClientSecret { get; private set; }
        /// <summary>
        /// 客户端显示名称
        /// </summary>
        [DisplayName("客户端显示名称")]
        public string ClientName { get; private set; }
        /// <summary>
        /// 客户端类型
        /// </summary>
        [DisplayName("客户端类型")]
        public string ClientGrantType { get; private set; }
        /// <summary>
        /// 密钥类型
        /// </summary>
        [DisplayName("密钥类型")]
        public string SecretType { get; private set; }
        /// <summary>
        /// 退出登录回调地址
        /// </summary>
        [DisplayName("退出登录回调地址")]
        public string PostLogoutRedirectUris { get; private set; }
        /// <summary>
        /// 登录重定向地址
        /// </summary>
        [DisplayName("登录重定向地址")]
        public string RedirectUris { get;private set; }
        /// <summary>
        /// 属性配置
        /// </summary>
        [DisplayName("属性配置")]
        public string Properties { get; private set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public string Description { get; private set; }
        /// <summary>
        /// 客户端访问作用域
        /// </summary>
        [DisplayName("客户端访问作用域")]
        public string ClientScopes { get; private set; }
        /// <summary>
        /// 协议类型
        /// </summary>
        [DisplayName("协议类型")]
        public string ProtocolType { get; set; }
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

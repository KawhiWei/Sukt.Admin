using Sukt.Module.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Sukt.Module.Core;

namespace Sukt.Core.Domain.Models.IdentityServerFour
{
    /// <summary>
    /// 客户端实体
    /// </summary>
    [DisplayName("客户端")]
    public class Client : AggregateRootBase<Guid>, IFullAuditedEntity<Guid>
    {
        public Client(string clientId, string clientName, bool allowAccessTokensViaBrowser, bool allowOfflineAccess)
        {
            ClientId = clientId;
            ClientName = clientName;
            AllowAccessTokensViaBrowser = allowAccessTokensViaBrowser;
            AllowOfflineAccess = allowOfflineAccess;
        }
        #region IdentityServer4 Client对象属性
        /// <summary>
        /// 是否启用
        /// </summary>
        [DisplayName("是否启用")]
        public bool Enabled { get; private set; } = true;
        /// <summary>
        /// 客户端Id
        /// </summary>
        [DisplayName("客户端Id")]
        public string ClientId { get; private set; }
        /// <summary>
        /// 客户端名称
        /// </summary>
        [DisplayName("客户端名称")]
        public string ClientName { get; private set; }
        /// <summary>
        /// 协议类型
        /// </summary>
        [DisplayName("协议类型")]
        public string ProtocolType { get; private set; } = "oidc";
        /// <summary>
        /// 需要客户端密码
        /// </summary>
        [DisplayName("需要客户端密码")]
        public bool RequireClientSecret { get; private set; } = true;
        /// <summary>
        /// 说明
        /// </summary>
        [DisplayName("说明")]
        public string Description { get; private set; }
        /// <summary>
        /// 客户端Uri
        /// </summary>
        [DisplayName("客户端Uri")]
        public string ClientUri { get; private set; }
        /// <summary>
        /// 徽标Uri
        /// </summary>
        [DisplayName("徽标Uri")]
        public string LogoUri { get; private set; }
        /// <summary>
        /// 需要同意
        /// </summary>
        [DisplayName("需要同意")]
        public bool RequireConsent { get; private set; }
        /// <summary>
        /// 是否允许记住同意
        /// </summary>
        [DisplayName("是否允许记住同意")]
        public bool AllowRememberConsent { get; private set; } = true;
        /// <summary>
        /// 始终包括用户声明
        /// </summary>
        [DisplayName("始终包括用户声明")]
        public bool AlwaysIncludeUserClaimsInIdToken { get; private set; }
        /// <summary>
        /// 是否需要Pkce
        /// </summary>
        [DisplayName("是否需要Pkce")]
        public bool RequirePkce { get; private set; } = true;
        /// <summary>
        /// 是否允许纯文本包
        /// </summary>
        [DisplayName("是否允许纯文本包")]
        public bool AllowPlainTextPkce { get; private set; }
        /// <summary>
        /// 是否需要请求对象
        /// </summary>
        [DisplayName("是否需要请求对象")]
        public bool RequireRequestObject { get; private set; }
        /// <summary>
        /// 是否允许通过浏览器访问令牌
        /// </summary>
        [DisplayName("是否允许通过浏览器访问令牌")]
        public bool AllowAccessTokensViaBrowser { get; private set; }
        /// <summary>
        /// 前端注销URI
        /// </summary>
        [DisplayName("前端注销URI")]
        public string FrontChannelLogoutUri { get; private set; }
        /// <summary>
        /// 是否需要前端频道注销会话
        /// </summary>
        [DisplayName("是否需要前端频道注销会话")]
        public bool FrontChannelLogoutSessionRequired { get; private set; } = true;
        /// <summary>
        /// 后台频道注销Uri
        /// </summary>
        [DisplayName("后台频道注销Uri")]
        public string BackChannelLogoutUri { get; private set; }
        /// <summary>
        /// 是否需要后台频道注销会话
        /// </summary>
        [DisplayName("是否需要后台频道注销会话")]
        public bool BackChannelLogoutSessionRequired { get; private set; } = true;
        /// <summary>
        /// 允许脱机访问
        /// </summary>
        [DisplayName("允许脱机访问")]
        public bool AllowOfflineAccess { get; private set; }
        /// <summary>
        /// 标识令牌生存期
        /// </summary>
        [DisplayName("标识令牌生存期")]
        public int IdentityTokenLifetime { get; private set; } = 300;
        /// <summary>
        /// 允许的身份令牌签名算法
        /// </summary>
        [DisplayName("允许的身份令牌签名算法")]
        public string AllowedIdentityTokenSigningAlgorithms { get; private set; }
        /// <summary>
        /// 访问令牌生存期
        /// </summary>
        [DisplayName("访问令牌生存期")]
        public int AccessTokenLifetime { get; private set; } = 3600;
        /// <summary>
        /// 授权
        /// </summary>
        [DisplayName("授权")]
        public int AuthorizationCodeLifetime { get; private set; } = 300;
        /// <summary>
        /// 终身同意书
        /// </summary>
        [DisplayName("终身同意书")]
        public int? ConsentLifetime { get; private set; }
        /// <summary>
        /// 绝对刷新令牌生存期
        /// </summary>
        [DisplayName("绝对刷新令牌生存期")]
        public int AbsoluteRefreshTokenLifetime { get; private set; } = 2592000;
        /// <summary>
        /// 滑动刷新令牌生存期
        /// </summary>
        [DisplayName("滑动刷新令牌生存期")]
        public int SlidingRefreshTokenLifetime { get; private set; } = 1296000;
        /// <summary>
        /// 滑动刷新令牌生存期
        /// </summary>
        [DisplayName("滑动刷新令牌生存期")]
        public int RefreshTokenUsage { get; private set; } = 1;

        /// <summary>
        /// 刷新时更新访问令牌声明
        /// </summary>
        [DisplayName("刷新时更新访问令牌声明")]
        public bool UpdateAccessTokenClaimsOnRefresh { get; private set; }
        /// <summary>
        /// 刷新令牌过期
        /// </summary>
        [DisplayName("刷新令牌过期")]
        public int RefreshTokenExpiration { get; private set; } = 1;
        /// <summary>
        /// 访问令牌类型
        /// </summary>
        [DisplayName("访问令牌类型")]
        public int AccessTokenType { get; private set; }

        /// <summary>
        /// 启用本地登录
        /// </summary>
        [DisplayName("启用本地登录")]
        public bool EnableLocalLogin { get; private set; } = true;

        /// <summary>
        /// 包括JwtId
        /// </summary>
        [DisplayName("包括JwtId")]
        public bool IncludeJwtId { get; private set; }

        /// <summary>
        /// 始终发送客户端声明
        /// </summary>
        [DisplayName("始终发送客户端声明")]
        public bool AlwaysSendClientClaims { get; private set; }

        /// <summary>
        /// 客户端声明前缀
        /// </summary>
        [DisplayName("客户端声明前缀")]
        public string ClientClaimsPrefix { get; private set; } = "client_";

        /// <summary>
        /// 成对主题盐
        /// </summary>
        [DisplayName("成对主题盐")]
        public string PairWiseSubjectSalt { get; private set; }

        /// <summary>
        /// 上次访问时间
        /// </summary>
        [DisplayName("上次访问时间")]
        public DateTime? LastAccessed { get; private set; }

        /// <summary>
        /// 用户SSO期限
        /// </summary>
        [DisplayName("用户SSO期限")]
        public int? UserSsoLifetime { get; private set; }

        /// <summary>
        /// 用户代码类型
        /// </summary>
        [DisplayName("用户代码类型")]
        public string UserCodeType { get; private set; }

        /// <summary>
        /// 设备代码期限
        /// </summary>
        [DisplayName("设备代码期限")]
        public int DeviceCodeLifetime { get; private set; } = 300;

        /// <summary>
        /// 不可编辑
        /// </summary>
        [DisplayName("不可编辑")]
        public bool NonEditable { get; private set; }
        #endregion

        #region IdentityServer4 CLient 导航属性

        public List<ClientClaim> Claims { get; private set; }

        public List<ClientCorsOrigin> AllowedCorsOrigins { get; private set; }

        public List<ClientProperty> Properties { get; private set; }

        public List<ClientIdPRestriction> IdentityProviderRestrictions { get; private set; }

        public List<ClientRedirectUri> RedirectUris { get; private set; }

        public List<ClientPostLogoutRedirectUri> PostLogoutRedirectUris { get; private set; }

        public List<ClientSecret> ClientSecrets { get; private set; }

        public List<ClientScope> AllowedScopes { get; private set; }

        public List<ClientGrantType> AllowedGrantTypes { get; private set; }

        #endregion IdentityServer4原始导航属性
        #region 业务处理函数
        /// <summary>
        /// 添加客户端授权类型
        /// </summary>
        /// <param name="allowedGrantTypes"></param>
        public void AddGrantTypes(List<string> allowedGrantTypes)
        {
            if (AllowedGrantTypes == null)
                AllowedGrantTypes = new List<ClientGrantType>();
            AllowedGrantTypes.AddRange(allowedGrantTypes.Select(x => new ClientGrantType(x)));
        }
        /// <summary>
        /// 添加客户端密钥
        /// </summary>
        /// <param name="clientSecrets"></param>
        public void AddClientSecrets(ClientSecret clientSecret)
        {
            if (ClientSecrets == null)
                ClientSecrets = new List<ClientSecret>();
            ClientSecrets.Add(clientSecret);
        }
        /// <summary>
        /// 添加客户端允许访问范围
        /// </summary>
        /// <param name="clientSecrets"></param>
        public void AddClientScopes(List<string> allowed)
        {
            if (AllowedScopes == null)
                AllowedScopes = new List<ClientScope>();
            AllowedScopes.ForEach(x =>
            {
                x.IsDeleted = true;
            });
            AllowedScopes.AddRange(allowed.Select(x => new ClientScope(x)));
        }
        /// <summary>
        /// 添加退出登录Uri
        /// </summary>
        /// <param name="allowed"></param>
        public void AddPostLogoutRedirectUris(List<string> allowed)
        {
            if (PostLogoutRedirectUris == null)
                PostLogoutRedirectUris = new List<ClientPostLogoutRedirectUri>();
            PostLogoutRedirectUris.ForEach(x =>
            {
                x.IsDeleted = true;
            });
            PostLogoutRedirectUris.AddRange(allowed.Select(x => new ClientPostLogoutRedirectUri(x)));
        }
        /// <summary>
        /// 添加登录回跳Uri
        /// </summary>
        /// <param name="allowed"></param>
        public void AddRedirectUris(List<string> allowed)
        {
            if (RedirectUris == null)
                RedirectUris = new List<ClientRedirectUri>();
            RedirectUris.ForEach(x =>
            {
                x.IsDeleted = true;
            });
            RedirectUris.AddRange(allowed.Select(x => new ClientRedirectUri(x)));
        }
        /// <summary>
        /// 添加跨域登录Uri
        /// </summary>
        /// <param name="allowed"></param>
        public void AddCorsOrigins(List<string> allowed)
        {
            if (AllowedCorsOrigins == null)
                AllowedCorsOrigins = new List<ClientCorsOrigin>();
            AllowedCorsOrigins.ForEach(x =>
            {
                x.IsDeleted = true;
            });
            AllowedCorsOrigins.AddRange(allowed.Select(x => new ClientCorsOrigin(x)));
        }

        #endregion

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

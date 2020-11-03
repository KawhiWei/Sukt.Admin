using Sukt.Core.Shared.Entity;
using System;
using System.ComponentModel;

namespace Sukt.Core.IdentityServerFour
{
    /// <summary>
    /// 客户端实体
    /// </summary>
    [DisplayName("客户端")]
    public abstract class ClientBase : IEntity<Guid>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [DisplayName("主键")]
        public Guid Id { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [DisplayName("是否启用")]
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// 客户端Id
        /// </summary>
        [DisplayName("客户端Id")]
        public string ClientId { get; set; }

        /// <summary>
        /// 协议类型
        /// </summary>
        [DisplayName("协议类型")]
        public string ProtocolType { get; set; } = "oidc";

        /// <summary>
        /// 需要客户端密码
        /// </summary>
        [DisplayName("需要客户端密码")]
        public bool RequireClientSecret { get; set; } = true;

        /// <summary>
        /// 客户端名称
        /// </summary>
        [DisplayName("客户端名称")]
        public string ClientName { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [DisplayName("说明")]
        public string Description { get; set; }

        /// <summary>
        /// 客户端Uri
        /// </summary>
        [DisplayName("客户端Uri")]
        public string ClientUri { get; set; }

        /// <summary>
        /// 徽标Uri
        /// </summary>
        [DisplayName("徽标Uri")]
        public string LogoUri { get; set; }

        /// <summary>
        /// 需要同意
        /// </summary>
        [DisplayName("需要同意")]
        public bool RequireConsent { get; set; }

        /// <summary>
        /// 是否允许记住同意
        /// </summary>
        [DisplayName("是否允许记住同意")]
        public bool AllowRememberConsent { get; set; } = true;

        /// <summary>
        /// 始终包括用户声明
        /// </summary>
        [DisplayName("始终包括用户声明")]
        public bool AlwaysIncludeUserClaimsInIdToken { get; set; }

        /// <summary>
        /// 是否需要Pkce
        /// </summary>
        [DisplayName("是否需要Pkce")]
        public bool RequirePkce { get; set; } = true;

        /// <summary>
        /// 是否允许纯文本包
        /// </summary>
        [DisplayName("是否允许纯文本包")]
        public bool AllowPlainTextPkce { get; set; }

        /// <summary>
        /// 是否需要请求对象
        /// </summary>
        [DisplayName("是否需要请求对象")]
        public bool RequireRequestObject { get; set; }

        /// <summary>
        /// 是否允许通过浏览器访问令牌
        /// </summary>
        [DisplayName("是否允许通过浏览器访问令牌")]
        public bool AllowAccessTokensViaBrowser { get; set; }

        /// <summary>
        /// 前端注销URI
        /// </summary>
        [DisplayName("前端注销URI")]
        public string FrontChannelLogoutUri { get; set; }

        /// <summary>
        /// 是否需要前端频道注销会话
        /// </summary>
        [DisplayName("是否需要前端频道注销会话")]
        public bool FrontChannelLogoutSessionRequired { get; set; } = true;

        /// <summary>
        /// 后台频道注销Uri
        /// </summary>
        [DisplayName("后台频道注销Uri")]
        public string BackChannelLogoutUri { get; set; }

        /// <summary>
        /// 是否需要后台频道注销会话
        /// </summary>
        [DisplayName("是否需要后台频道注销会话")]
        public bool BackChannelLogoutSessionRequired { get; set; } = true;

        /// <summary>
        /// 允许脱机访问
        /// </summary>
        [DisplayName("允许脱机访问")]
        public bool AllowOfflineAccess { get; set; }

        /// <summary>
        /// 标识令牌生存期
        /// </summary>
        [DisplayName("标识令牌生存期")]
        public int IdentityTokenLifetime { get; set; } = 300;

        /// <summary>
        /// 允许的身份令牌签名算法
        /// </summary>
        [DisplayName("允许的身份令牌签名算法")]
        public string AllowedIdentityTokenSigningAlgorithms { get; set; }

        /// <summary>
        /// 访问令牌生存期
        /// </summary>
        [DisplayName("访问令牌生存期")]
        public int AccessTokenLifetime { get; set; } = 3600;

        /// <summary>
        /// 授权
        /// </summary>
        [DisplayName("授权")]
        public int AuthorizationCodeLifetime { get; set; } = 300;

        /// <summary>
        /// 终身同意书
        /// </summary>
        [DisplayName("终身同意书")]
        public int? ConsentLifetime { get; set; }

        /// <summary>
        /// 绝对刷新令牌生存期
        /// </summary>
        [DisplayName("绝对刷新令牌生存期")]
        public int AbsoluteRefreshTokenLifetime { get; set; } = 2592000;

        /// <summary>
        /// 滑动刷新令牌生存期
        /// </summary>
        [DisplayName("滑动刷新令牌生存期")]
        public int SlidingRefreshTokenLifetime { get; set; } = 1296000;

        /// <summary>
        /// 滑动刷新令牌生存期
        /// </summary>
        [DisplayName("滑动刷新令牌生存期")]
        public int RefreshTokenUsage { get; set; } = 1;

        /// <summary>
        /// 刷新时更新访问令牌声明
        /// </summary>
        [DisplayName("刷新时更新访问令牌声明")]
        public bool UpdateAccessTokenClaimsOnRefresh { get; set; }

        /// <summary>
        /// 刷新令牌过期
        /// </summary>
        [DisplayName("刷新令牌过期")]
        public int RefreshTokenExpiration { get; set; } = 1;

        /// <summary>
        /// 访问令牌类型
        /// </summary>
        [DisplayName("访问令牌类型")]
        public int AccessTokenType { get; set; }

        /// <summary>
        /// 启用本地登录
        /// </summary>
        [DisplayName("启用本地登录")]
        public bool EnableLocalLogin { get; set; } = true;

        /// <summary>
        /// 包括JwtId
        /// </summary>
        [DisplayName("包括JwtId")]
        public bool IncludeJwtId { get; set; }

        /// <summary>
        /// 始终发送客户端声明
        /// </summary>
        [DisplayName("始终发送客户端声明")]
        public bool AlwaysSendClientClaims { get; set; }

        /// <summary>
        /// 客户端声明前缀
        /// </summary>
        [DisplayName("客户端声明前缀")]
        public string ClientClaimsPrefix { get; set; } = "client_";

        /// <summary>
        /// 成对主题盐
        /// </summary>
        [DisplayName("成对主题盐")]
        public string PairWiseSubjectSalt { get; set; }

        /// <summary>
        /// 上次访问时间
        /// </summary>
        [DisplayName("上次访问时间")]
        public DateTime? LastAccessed { get; set; }

        /// <summary>
        /// 用户SSO期限
        /// </summary>
        [DisplayName("用户SSO期限")]
        public int? UserSsoLifetime { get; set; }

        /// <summary>
        /// 用户代码类型
        /// </summary>
        [DisplayName("用户代码类型")]
        public string UserCodeType { get; set; }

        /// <summary>
        /// 设备代码期限
        /// </summary>
        [DisplayName("设备代码期限")]
        public int DeviceCodeLifetime { get; set; } = 300;

        /// <summary>
        /// 不可编辑
        /// </summary>
        [DisplayName("不可编辑")]
        public bool NonEditable { get; set; }

        #region IdentityServer4原始导航属性

        //public List<ClientClaim> Claims { get; set; }

        //public List<ClientCorsOrigin> AllowedCorsOrigins { get; set; }

        //public List<ClientProperty> Properties { get; set; }

        //public List<ClientIdPRestriction> IdentityProviderRestrictions { get; set; }

        //public List<ClientRedirectUri> RedirectUris { get; set; }

        //public List<ClientPostLogoutRedirectUri> PostLogoutRedirectUris { get; set; }

        //public List<ClientSecret> ClientSecrets { get; set; }

        //public List<ClientScope> AllowedScopes { get; set; }

        //public List<ClientGrantType> AllowedGrantTypes { get; set; }

        #endregion IdentityServer4原始导航属性
    }
}
using Sukt.Core.Dtos.IdentityServer4Dto.Enums;
using System.Collections.Generic;
using System.ComponentModel;

namespace Sukt.Core.Dtos.IdentityServer4Dto
{
    public class ClientInputDto
    {
        public ClientInputDto()
        {
            AllowedGrantTypes = new List<string>();
        }
        [DisplayName("客户端类型")]
        public ClientTypeEnum ClientType { get; set; }
        /// <summary>
        /// 客户端Id
        /// </summary>
        [DisplayName("客户端Id")]
        public string ClientId { get; set; }
        /// <summary>
        /// 客户端名称
        /// </summary>
        [DisplayName("客户端名称")]
        public string ClientName { get; set; }
        /// <summary>
        /// 是否允许通过浏览器访问令牌
        /// </summary>
        [DisplayName("是否允许通过浏览器访问令牌")]
        public bool AllowAccessTokensViaBrowser { get; set; }
        /// <summary>
        /// 是否返回刷新Token
        /// </summary>
        public bool AllowOfflineAccess { get; set; }
        ///// <summary>
        ///// 客户端令牌
        ///// </summary>
        //[DisplayName("客户端令牌")]
        //public string ClientSecrets { get; set; }
        /// <summary>
        /// 授权类型
        /// </summary>
        public List<string> AllowedGrantTypes { get; private set; }
        ///// <summary>
        ///// 回调地址
        ///// </summary>
        //public List<string> RedirectUris { get; set; }
        ///// <summary>
        ///// 退出登录回调地址
        ///// </summary>
        //public List<string> PostLogoutRedirectUris { get; set; }
        ///// <summary>
        ///// 跨域地址
        ///// </summary>
        //public List<string> AllowedCorsOrigins { get; set; }
        ///// <summary>
        ///// 客户端访问作用域
        ///// </summary>
        //public List<string> AllowedScopes { get; set; }
    }
}

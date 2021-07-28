using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Validation.Response
{
    /// <summary>
    /// Token 返回体
    /// </summary>
    public class TokenResponse
    {
        /// <summary>
        /// 身份标识Token
        /// </summary>
        public string IdentityToken { get; set; }
        /// <summary>
        /// Access Token
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// Token有效时间
        /// </summary>
        public int AccessTokenLifetime { get; set; }
        /// <summary>
        /// 刷新Token
        /// </summary>
        public string RefreshToken { get; set; }
        /// <summary>
        /// 客户端作用域
        /// </summary>
        public string Scope { get; set; }
        /// <summary>
        /// 自定义字典
        /// </summary>
        public Dictionary<string, object> Custom { get; set; } = new Dictionary<string, object>();
    }
}

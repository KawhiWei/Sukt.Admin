using Sukt.AuthServer.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sukt.Module.Core.OidcConstants;

namespace Sukt.AuthServer.EndpointHandler.TokenError
{
    /// <summary>
    /// Token错误信息返回的Response
    /// </summary>
    public class TokenErrorResponse
    {
        /// <summary>
        /// 返回错误信息
        /// </summary>
        public string Error { get; set; } = TokenErrors.InvalidRequest;
        /// <summary>
        /// 错误信息说明
        /// </summary>
        public string ErrorDescription { get; set; }
        /// <summary>
        /// 自定义返回信息
        /// </summary>
        public Dictionary<string, object> Custom { get; set; } = new Dictionary<string, object>();
    }
}

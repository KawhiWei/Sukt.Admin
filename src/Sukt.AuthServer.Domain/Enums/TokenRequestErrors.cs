using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Domain.Enums
{
    /// <summary>
    /// 获取token返回错误信息枚举
    /// </summary>
    public enum TokenRequestErrors
    {
        /// <summary>
        /// invalid_request
        /// </summary>
        InvalidRequest,
        /// <summary>
        /// invalid_client
        /// </summary>
        InvalidClient,
        /// <summary>
        /// invalid_grant
        /// </summary>
        InvalidGrant,
        /// <summary>
        /// unauthorized_client
        /// </summary>
        UnauthorizedClient,
        /// <summary>
        /// unsupported_grant_type
        /// </summary>
        UnsupportedGrantType,
        /// <summary>
        /// invalid_scope
        /// </summary>
        InvalidScope,
        /// <summary>
        /// invalid_target
        /// </summary>
        InvalidTarget
    }
}

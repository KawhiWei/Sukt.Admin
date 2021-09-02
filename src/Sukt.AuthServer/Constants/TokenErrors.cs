using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Constants
{
    public static class TokenErrors
    {
        public const string InvalidRequest = "invalid_request——无效的请求";
        public const string InvalidClient = "invalid_client——无效的客户端";
        public const string InvalidGrant = "invalid_grant——无效的授权";
        public const string UnauthorizedClient = "unauthorized_client——无效的客户端类型";
        public const string UnsupportedGrantType = "unsupported_grant_type";
        public const string UnsupportedResponseType = "unsupported_response_type";
        public const string InvalidScope = "invalid_scope——无效授权的作用域";
        public const string AuthorizationPending = "authorization_pending";
        public const string AccessDenied = "access_denied";
        public const string SlowDown = "slow_down";
        public const string ExpiredToken = "expired_token";
        public const string InvalidTarget = "invalid_target";

    }
}

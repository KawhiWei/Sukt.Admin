using Sukt.AuthServer.Domain.Enums;
using Sukt.Module.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Validation.ValidationResult
{
    public class GrantValidationResult: ValidationResult
    {
        public GrantValidationResult(Dictionary<string, object> customResponse)
        {
            IsError = false;
            CustomResponse = customResponse;
        }

        public GrantValidationResult(ClaimsPrincipal subject, Dictionary<string, object> customResponse)
        {
            IsError = false;
            if (subject.Identities.Count() != 1) throw new InvalidOperationException("only a single identity supported——仅支持单个标识");
            if (subject.FindFirst(JwtClaimTypes.Subject) == null) throw new InvalidOperationException("sub claim is missing——主题信息不存在");
            if (subject.FindFirst(JwtClaimTypes.IdentityProvider) == null) throw new InvalidOperationException("idp claim is missing——认证地址不存在");
            if (subject.FindFirst(JwtClaimTypes.AuthenticationMethod) == null) throw new InvalidOperationException("amr claim is missing——身份认证方法不存在");
            if (subject.FindFirst(JwtClaimTypes.AuthenticationTime) == null) throw new InvalidOperationException("auth_time claim is missing——认证时间不存在");
            Subject = subject;
            CustomResponse = customResponse;
        }

        /// <summary>
        /// 用户登陆主体信息
        /// </summary>
        public ClaimsPrincipal Subject { get; set; }
        /// <summary>
        /// 自定义Response信息返回
        /// </summary>
        public Dictionary<string, object> CustomResponse { get; set; } = new Dictionary<string, object>();
        public GrantValidationResult(TokenRequestErrors error, string errorDescription = null, Dictionary<string, object> customResponse = null)
        {
            Error = ConvertTokenErrorEnumToString(error);
            ErrorDescription = errorDescription;
            CustomResponse = customResponse;
        }
        private string ConvertTokenErrorEnumToString(TokenRequestErrors error)
        {
            return error switch
            {
                TokenRequestErrors.InvalidClient => OidcConstants.TokenErrors.InvalidClient,
                TokenRequestErrors.InvalidGrant => OidcConstants.TokenErrors.InvalidGrant,
                TokenRequestErrors.InvalidRequest => OidcConstants.TokenErrors.InvalidRequest,
                TokenRequestErrors.InvalidScope => OidcConstants.TokenErrors.InvalidScope,
                TokenRequestErrors.UnauthorizedClient => OidcConstants.TokenErrors.UnauthorizedClient,
                TokenRequestErrors.UnsupportedGrantType => OidcConstants.TokenErrors.UnsupportedGrantType,
                TokenRequestErrors.InvalidTarget => OidcConstants.TokenErrors.InvalidTarget,
                _ => throw new InvalidOperationException("invalid token error")
            };
        }
        public GrantValidationResult(
            string subject,
            string authenticationMethod,
            DateTime authTime,
            IEnumerable<Claim> claims = null,
            string identityProvider = IdentityServerConstants.LocalIdentityProvider,
            Dictionary<string, object> customResponse = null)
        {
            IsError = false;

            var resultClaims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Subject, subject),
                new Claim(JwtClaimTypes.AuthenticationMethod, authenticationMethod),
                new Claim(JwtClaimTypes.IdentityProvider, identityProvider),
                new Claim(JwtClaimTypes.AuthenticationTime, new DateTimeOffset(authTime).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };

            if (claims!=null)
            {
                resultClaims.AddRange(claims);
            }

            var id = new ClaimsIdentity(authenticationMethod);
            id.AddClaims(resultClaims.Distinct());

            Subject = new ClaimsPrincipal(id);
            CustomResponse = customResponse;
        }
    }
}

using Microsoft.Extensions.Logging;
using Sukt.AuthServer.Extensions;
using Sukt.AuthServer.Validation.ValidationResult;
using Sukt.Module.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Generator
{
    public class DefaultClaimsService : IClaimsService
    {
        protected readonly ILogger Logger;

        public DefaultClaimsService(ILogger<DefaultClaimsService> logger)
        {
            Logger = logger;
        }

        public virtual async Task<IEnumerable<Claim>> GetAccessTokenClaimsAsync(ClaimsPrincipal subject,/* ResourceValidationResult resources,*/  ValidatedRequest request)
        {
            await Task.CompletedTask;
            var outputClaims = new List<Claim>
            {
                new Claim(JwtClaimTypes.ClientId, request.ClientId)
            };
            if (!string.Equals(request.ClientId, request.ClientApplication.ClientId))
            {
                Logger.LogDebug("客户端 {clientId} 是冒充的 {impersonatedClientId}", request.ClientApplication.ClientId, request.ClientId);
            }
            //if (request.ClientClaims != null && request.ClientClaims.Any())
            //{
            //    if (subject == null || request.Client.AlwaysSendClientClaims)
            //    {
            //        foreach (var claim in request.ClientClaims)
            //        {
            //            var claimType = claim.Type;

            //            if (request.Client.ClientClaimsPrefix.IsPresent())
            //            {
            //                claimType = request.Client.ClientClaimsPrefix + claimType;
            //            }

            //            outputClaims.Add(new Claim(claimType, claim.Value, claim.ValueType));
            //        }
            //    }
            //}

            if(subject!=null)
            {
                Logger.LogDebug("获取主体访问令牌的声明 for subject: {subject}", subject.GetSubjectId());
                outputClaims.AddRange(GetStandardSubjectClaims(subject));
                outputClaims.AddRange(GetOptionalClaims(subject));
            }
            

            return outputClaims;
        }
        protected virtual IEnumerable<Claim> GetStandardSubjectClaims(ClaimsPrincipal subject)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Subject, subject.GetSubjectId()),
                new Claim(JwtClaimTypes.AuthenticationTime, subject.GetAuthenticationTimeEpoch().ToString(), ClaimValueTypes.Integer64),
                new Claim(JwtClaimTypes.IdentityProvider, subject.GetIdentityProvider())
            };

            claims.AddRange(subject.GetAuthenticationMethods());

            return claims;
        }
        protected virtual IEnumerable<Claim> GetOptionalClaims(ClaimsPrincipal subject)
        {
            var claims = new List<Claim>();

            var acr = subject.FindFirst(JwtClaimTypes.AuthenticationContextClassReference);
            if (acr != null) claims.Add(acr);

            return claims;
        }
    }
}

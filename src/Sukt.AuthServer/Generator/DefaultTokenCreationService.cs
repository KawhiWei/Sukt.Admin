using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using static Sukt.Module.Core.SuktAuthServerConstants;
using System.Text.Json;
using Sukt.Module.Core;
using System.Security.Claims;
using System.Globalization;

namespace Sukt.AuthServer.Generator
{
    public class DefaultTokenCreationService : ITokenCreationService
    {
        protected readonly ISystemClock Clock;
        protected readonly ISuktSigningCredentialStore SuktSigningCredentialStore;
        protected readonly ILogger Logger;

        public DefaultTokenCreationService(ISystemClock clock, ISuktSigningCredentialStore suktSigningCredentialStore, ILogger<DefaultTokenCreationService> logger)
        {
            this.Clock = clock;
            this.SuktSigningCredentialStore = suktSigningCredentialStore;
            this.Logger = logger;
        }

        public virtual async Task<string> CreateTokenAsync(TokenRequest request)
        {
            var header = await CreateHeaderAsync(request);
            var payload = await CreateJwtPayloadAsync(request);
            var resulttoken = await CreateJwtAsync(new JwtSecurityToken(header, payload));
            return resulttoken;
        }

        protected virtual async Task<JwtHeader> CreateHeaderAsync(TokenRequest request)
        {
            var credential = await SuktSigningCredentialStore.GetSigningCredentialsAsync();
            if (credential == null)
            {
                throw new InvalidOperationException("未配置签名凭据。不能创建JWT令牌");
            }
            var header = new JwtHeader(credential);
            if (credential.Key is X509SecurityKey x509Key)
            {
                var cert = x509Key.Certificate;
                if (Clock.UtcNow.UtcDateTime > cert.NotAfter)
                {
                    Logger.LogWarning("Certificate {subjectName} has expired on {expiration}", cert.Subject, cert.NotAfter.ToString(CultureInfo.InvariantCulture));
                }

                header["x5t"] = Sukt.AuthServer.Extensions.Base64Url.Encode(cert.GetCertHash());
            }
            if (request.Type == TokenTypes.AccessToken)
            {
                header["typ"] = "at+jwt";

            }
            return header;
        }
        protected virtual Task<JwtPayload> CreateJwtPayloadAsync(TokenRequest request)
        {
            var payload = new JwtPayload(
                request.Issuer,
                null, 
                null,
                Clock.UtcNow.UtcDateTime,
                Clock.UtcNow.UtcDateTime.AddSeconds(request.Lifetime)
                );
            foreach (var aud in request.Audiences)
            {
                payload.AddClaim(new Claim(JwtClaimTypes.Audience, aud));
            }
            var amrClamis = request.Subject.Where(c => c.Type == JwtClaimTypes.AuthenticationMethod).ToArray();
            var scopeClaims = request.Subject.Where(c => c.Type == JwtClaimTypes.Scope).ToArray();
            var jsonClaims = request.Subject.Where(x => x.ValueType == SuktAuthServerConstants.ClaimValueTypes.Json).ToList();
            var normaClaims = request.Subject.Except(amrClamis).Except(scopeClaims).Except(jsonClaims);
            payload.AddClaims(normaClaims);
            if (!scopeClaims.IsNullOrEmpty())
            {
                var scopeValues = scopeClaims.Select(x => x.Value).ToArray();
                payload.Add(JwtClaimTypes.Scope, scopeValues);
            }
            if (!amrClamis.IsNullOrEmpty())
            {
                var amrValues = amrClamis.Select(x => x.Value).Distinct().ToArray();
                payload.Add(JwtClaimTypes.AuthenticationMethod, amrValues);
            }
            return Task.FromResult(payload);
        }
        protected virtual Task<string> CreateJwtAsync(JwtSecurityToken jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            return Task.FromResult(handler.WriteToken(jwt));
        }
    }
}

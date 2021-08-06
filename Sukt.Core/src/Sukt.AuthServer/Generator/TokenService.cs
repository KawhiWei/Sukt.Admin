using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.JsonWebTokens;
using Newtonsoft.Json.Linq;
using Sukt.AuthServer.Extensions;
using Sukt.Module.Core;
using Sukt.Module.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Sukt.Module.Core.IdentityServerConstants;

namespace Sukt.AuthServer.Generator
{
    /// <summary>
    /// Token生成服务实现
    /// </summary>
    public class TokenService : ITokenService
    {
        protected readonly ILogger _logger;
        protected readonly ISystemClock _systemClock;
        protected readonly IHttpContextAccessor ContextAccessor;
        public TokenService(ILogger<TokenService> logger, ISystemClock systemClock, IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _systemClock = systemClock;
            ContextAccessor = contextAccessor;
        }

        public virtual async Task<TokenRequest> CreateTokenRequestAsync(TokenCreationRequest request)
        {
            await Task.CompletedTask;
            _logger.LogTrace("创建 access token");
            var claims = new List<Claim>();
            claims.Add(new Claim(JwtClaimTypes.JwtId, CryptoRandom.CreateUniqueId(16, CryptoRandom.OutputFormat.Hex)));
            if (!request.ValidatedRequest.SessionId.IsNullOrWhiteSpace())
            {
                claims.Add(new Claim(JwtClaimTypes.SessionId, request.ValidatedRequest.SessionId));
            }
            claims.Add(new Claim(JwtClaimTypes.IssuedAt, _systemClock.UtcNow.ToUnixTimeMilliseconds().ToString(), System.Security.Claims.ClaimValueTypes.Integer64));
            var issuer =$"{ ContextAccessor.HttpContext.Request.Scheme }://{ContextAccessor.HttpContext.Request.Host.Value}";
            var token = new TokenRequest(TokenTypes.AccessToken)
            {
                CreationTime = DateTime.UtcNow,
                Issuer = issuer,
                Lifetime = request.ValidatedRequest.AccessTokenExpire,
                Claims = claims.Distinct().ToList(),
                SuktApplicationClientId = request.ValidatedRequest.ClientApplication.ClientId,
                TokenType = request.ValidatedRequest.TokenType,
            };
            foreach (var aud in request.ResourceValidation.SuktResources.SuktResources.Select(x => x.Name).Distinct())
            {
                token.Audiences.Add(aud);
            }
            return token;
        }

        public async Task<string> CreateAccessTokenAsync(TokenRequest request)
        {
            var payload = await CreateJwtPayloadAsync(request);
            //TODO: 设置credentials
            var handler = new JsonWebTokenHandler { SetDefaultTimesOnTokenCreation = false };
            var stre=handler.CreateToken(payload /*, credential*/);
            return stre /*handler.CreateToken(payload , credential)*/;
        }

        public Task<string> CreateJwtPayloadAsync(TokenRequest token)
        {
            try
            {
                var payload = new Dictionary<string, object>
                {
                    { JwtClaimTypes.Issuer, token.Issuer }
                };

                var now = _systemClock.UtcNow.ToUnixTimeSeconds();
                payload.Add(JwtClaimTypes.IssuedAt, now);
                payload.Add(JwtClaimTypes.NotBefore, now);
                payload.Add(JwtClaimTypes.Expiration, now + token.Lifetime);

                if (token.Audiences.Any())
                {
                    payload.Add(JwtClaimTypes.Audience,
                        token.Audiences.Count == 1 ?
                        token.Audiences.First() :
                        token.Audiences);
                }

                var scopeClaims = token.Claims.Where(c => c.Type == JwtClaimTypes.Scope).ToArray();
                if (scopeClaims.Any())
                {
                    payload.Add(JwtClaimTypes.Scope, scopeClaims.Select(c => c.Value).Distinct().ToArray());
                }

                var amrClaims = token.Claims.Where(c => c.Type == JwtClaimTypes.AuthenticationMethod).ToArray();
                if (amrClaims.Any())
                {
                    payload.Add(JwtClaimTypes.AuthenticationMethod,
                        amrClaims.Select(c => c.Value).Distinct().ToArray());
                }

                var otherClaimsTypes = token.Claims.Where(c => c.Type != JwtClaimTypes.Scope && c.Type != JwtClaimTypes.AuthenticationMethod)
                    .Select(c => c.Type).Distinct();
                foreach (var claimType in otherClaimsTypes)
                {
                    var claims = token.Claims.Where(c => c.Type == claimType).ToArray();
                    payload.Remove(claimType);
                    payload.Add(claimType, claims.Length == 1 ? claims[0] : claims);
                }

                return Task.FromResult(JsonSerializer.Serialize(payload));
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "创建时JWT payload发生错误");
                throw;
            }
        }

    }
}

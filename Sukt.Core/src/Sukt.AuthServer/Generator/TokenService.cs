using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Sukt.AuthServer.Extensions;
using Sukt.Module.Core;
using Sukt.Module.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Sukt.Module.Core.IdentityServerConstants;

namespace Sukt.AuthServer.Generator
{
    /// <summary>
    /// Token生成服务实现
    /// </summary>
    public class TokenService: ITokenService
    {
        protected readonly ILogger _logger;
        protected readonly ISystemClock _systemClock;
        public TokenService(ILogger<TokenService> logger,ISystemClock systemClock)
        {
            _logger = logger;
            _systemClock = systemClock;
        }

        public virtual async Task<TokenRequest> CreateAccessTokenAsync(TokenCreationRequest request)
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
            var issuer = "";
            var token = new TokenRequest(TokenTypes.AccessToken)
            {
                CreationTime = DateTime.UtcNow,
                Issuer = issuer,
                Lifetime = request.ValidatedRequest.AccessTokenExpire,
                Claims = claims.Distinct().ToList(),
                SuktApplicationClientId=request.ValidatedRequest.ClientApplication.ClientId,
                TokenType=request.ValidatedRequest.TokenType,
            };
            return token;
        }
    }
}

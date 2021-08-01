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

namespace Sukt.AuthServer.Generator
{
    /// <summary>
    /// Token生成服务实现
    /// </summary>
    public class TokenService: ITokenService
    {
        protected readonly ILogger _logger;

        public TokenService(ILogger<TokenService> logger)
        {
            _logger = logger;
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
            return new TokenRequest();
        }
    }
}

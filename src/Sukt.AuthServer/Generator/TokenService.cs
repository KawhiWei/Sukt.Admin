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
using static Sukt.Module.Core.SuktAuthServerConstants;

namespace Sukt.AuthServer.Generator
{
    /// <summary>
    /// Token生成服务实现
    /// </summary>
    public class TokenService : ITokenService
    {
        protected readonly ILogger _logger;
        protected readonly ISystemClock _systemClock;
        protected readonly IHttpContextAccessor HttpContextAccessor;
        protected readonly IClaimsService ClaimsService;
        protected readonly ITokenCreationService TokenCreationService;
        public TokenService(ILogger<TokenService> logger, ISystemClock systemClock, IHttpContextAccessor contextAccessor, IClaimsService claimsService, ITokenCreationService tokenCreationService)
        {
            _logger = logger;
            _systemClock = systemClock;
            HttpContextAccessor = contextAccessor;
            ClaimsService = claimsService;
            TokenCreationService = tokenCreationService;
        }

        public virtual async Task<TokenRequest> CreateTokenRequestAsync(TokenCreationRequest request)
        {
            await Task.CompletedTask;
            _logger.LogTrace("开始创建 access token");
            request.Validate();
            var claims = new List<Claim>();
            claims.AddRange(await ClaimsService.GetAccessTokenClaimsAsync(request.Subject, request.ResourceValidation, request.ValidatedRequest));
            claims.Add(new Claim(JwtClaimTypes.JwtId, CryptoRandom.CreateUniqueId(16, CryptoRandom.OutputFormat.Hex)));
            if (!request.ValidatedRequest.SessionId.IsNullOrWhiteSpace())
            {
                claims.Add(new Claim(JwtClaimTypes.SessionId, request.ValidatedRequest.SessionId));
            }
            claims.Add(new Claim(JwtClaimTypes.IssuedAt, _systemClock.UtcNow.ToUnixTimeMilliseconds().ToString(), System.Security.Claims.ClaimValueTypes.Integer64));
            var issuer =$"{ HttpContextAccessor.HttpContext.Request.Scheme }://{HttpContextAccessor.HttpContext.Request.Host.Value}";
            var token = new TokenRequest(TokenTypes.AccessToken)
            {
                CreationTime = DateTime.UtcNow,
                Issuer = issuer,
                Lifetime = request.ValidatedRequest.AccessTokenExpire,
                Subject = claims.Distinct().ToList(),
                SuktApplicationClientId = request.ValidatedRequest.ClientApplication.ClientId,
                TokenType = request.ValidatedRequest.TokenType,
            };
            foreach (var aud in request.ResourceValidation.SuktResources.SuktResources.Select(x => x.Name).Distinct())
            {
                token.Audiences.Add(aud);
            }
            return token;
        }
        /// <summary>
        /// 创建受保护的安全的AccessToken
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<string> CreateAccessTokenAsync(TokenRequest request)
        {
            //判断创建token的类型
            if(request.Type== OidcConstants.TokenTypes.AccessToken)
            {
                if(request.TokenType== TokenType.Jwt)
                {

                }
            }
            var stre=await  TokenCreationService.CreateTokenAsync(request);
            return stre;
        }
    }
}

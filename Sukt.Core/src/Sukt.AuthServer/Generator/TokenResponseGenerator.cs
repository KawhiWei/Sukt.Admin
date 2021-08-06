using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Sukt.AuthServer.EndpointHandler.EndpointHandlerResult;
using Sukt.AuthServer.EndpointHandler.TokenError;
using Sukt.AuthServer.Validation.Response;
using Sukt.AuthServer.Validation.ValidationResult;
using Sukt.Core.Domain.Models.IdentityServerFour;
using Sukt.Module.Core;
using Sukt.Module.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Generator
{
    public class TokenResponseGenerator : ITokenResponseGenerator
    {
        private readonly ILogger _logger;
        private readonly ITokenService _tokenService;
        public TokenResponseGenerator(ILogger<TokenResponseGenerator> logger, ITokenService tokenService)
        {
            _logger = logger;
            _tokenService = tokenService;
        }

        public virtual async Task<TokenResponse> ProcessAsync(TokenRequestValidationResult request)
        {
            _logger.LogTrace("Processing token request——处理获取token请求.");
            return request.ValidatedRequest.GrantType switch
            {
                GrantType.ResourceOwnerPassword => await ProcessPasswordRequestAsync(request),
                _ => await ProcessExtensionGrantRequestAsync(request),
            };
        }
        private TokenErrorResult Error(string error, string errorDescription = null, Dictionary<string, object> custom = null)
        {
            var response = new TokenErrorResponse
            {
                Error = error,
                ErrorDescription = errorDescription,
                Custom = custom
            };
            return new TokenErrorResult(response);
        }
        protected virtual async Task<TokenResponse> ProcessPasswordRequestAsync(TokenRequestValidationResult request)
        {
            _logger.LogDebug($"为密码请求方式创建返回响应");
            return await ProcessTokenRequestAsync(request);
        }
        /// <summary>
        /// Creates the response for a token request.
        /// </summary>
        /// <param name="validationResult">The validation result.</param>
        /// <returns></returns>
        protected virtual async Task<TokenResponse> ProcessTokenRequestAsync(TokenRequestValidationResult validationResult)
        {
            (var accessToken, var refreshToken) = await CreateAccessTokenAsync(validationResult.ValidatedRequest);
            var response = new TokenResponse
            {
                AccessToken = accessToken,
                AccessTokenLifetime = validationResult.ValidatedRequest.AccessTokenExpire,
                Custom = validationResult.CustomResponse,
                //Scope = validationResult.ValidatedRequest.ValidatedResources.RawScopeValues.ToSpaceSeparatedString()
            };

            if (refreshToken.IsNullOrWhiteSpace())
            {
                response.RefreshToken = refreshToken;
            }

            return response;
        }
        protected virtual async Task<(string accessToken, string refreshToken)> CreateAccessTokenAsync(ValidatedTokenRequest request)
        {
            TokenCreationRequest tokenCreationRequest;
            bool createRefreshToken;
            //To do  判断是否是授权码 AuthorizationCode 或 DeviceCode 模式
            createRefreshToken = request.IsRefreshToken;
            tokenCreationRequest = new TokenCreationRequest
            {
                Subject = request.Subject,
                ResourceValidation = request.ResourceValidation,
                ValidatedRequest = request
            };
            var at = "";
            var tokenRequest = await _tokenService.CreateTokenRequestAsync(tokenCreationRequest);
            var accessToken = await _tokenService.CreateAccessTokenAsync(tokenRequest);//await TokenService.CreateSecurityTokenAsync(at);

            if (createRefreshToken)
            {
                //var refreshToken = await RefreshTokenService.CreateRefreshTokenAsync(tokenRequest.Subject, at, request.Client);
                return (accessToken, /*refreshToken*/"");
            }

            return (accessToken, null);
        }

        protected virtual Task<TokenResponse> ProcessExtensionGrantRequestAsync(TokenRequestValidationResult request)
        {
            _logger.LogTrace("Creating response for extension grant request");
            return ProcessTokenRequestAsync(request);
        }
    }
}

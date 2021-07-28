using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Sukt.AuthServer.Constants;
using Sukt.AuthServer.Contexts;
using Sukt.AuthServer.EndpointHandler.EndpointHandlerResult;
using Sukt.AuthServer.Extensions;
using Sukt.AuthServer.Validation.ValidationResult;
using Sukt.Module.Core;
using Sukt.Module.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sukt.Module.Core.IdentityServerConstants;
using static Sukt.Module.Core.OidcConstants;

namespace Sukt.AuthServer.Validation
{
    public class TokenRequestValidator : ITokenRequestValidator
    {
        private ValidatedTokenRequest _validatedRequest;
        private readonly ILogger _logger;

        public TokenRequestValidator(ILogger<TokenRequestValidator> logger)
        {
            _logger = logger;
        }

        public async Task<TokenRequestValidationResult> ValidateRequestAsync(NameValueCollection parameters, ClientSecretValidationResult clientValidationResult)
        {
            _validatedRequest = new ValidatedTokenRequest()
            {
                Raw=parameters,
            };
            await Task.CompletedTask;
            if (clientValidationResult.ClientApplication.ProtocolType != ProtocolTypes.OpenIdConnect)
            {
                _logger.LogError($"无效的客户端协议类型——clientId:{clientValidationResult.ClientApplication.ClientId},expectedProtocolType{clientValidationResult.ClientApplication.ProtocolType},");
                return InvalidError(TokenErrors.InvalidClient);
            }
            var grantType = parameters.Get(TokenRequest.GrantType);
            if (grantType.IsNullOrEmpty())
            {
                _logger.LogError($"无效的授权——clientId:{clientValidationResult.ClientApplication.ClientId},expectedProtocolType{clientValidationResult.ClientApplication.ProtocolType},");
                return InvalidError(TokenErrors.InvalidGrant);
            }
            _validatedRequest.GrantType = grantType;
            switch (grantType)
            {
                case GrantType.ResourceOwnerPassword:
                    return await RunValidationAsync(RunValidateResourceOwnerCredentialRequestAsync, parameters);
                default:
                    return new TokenRequestValidationResult(null);
            }

        }
        /// <summary>
        /// 委托运行对应的处理方法
        /// </summary>
        /// <param name="varlidationFunc"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private async Task<TokenRequestValidationResult> RunValidationAsync(Func<NameValueCollection, Task<TokenRequestValidationResult>> varlidationFunc, NameValueCollection parameters)
        {
            var result = await varlidationFunc(parameters);
            if (result.IsError)
            {
                return result;
            }
            //暂未用到
            _logger.LogInformation($"调用自定义请求验证器:{result.IsError}asdasdsa");
            var customTokenValidationContext = new CustomTokenRequestValidationContext { Result = result };
            return customTokenValidationContext.Result;
        }
        /// <summary>
        /// 密码授权方式验证
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private async Task<TokenRequestValidationResult> RunValidateResourceOwnerCredentialRequestAsync(NameValueCollection parameters)
        {
            _logger.LogInformation("开始检验密码授权方式传入参数!");
            if (!_validatedRequest.ClientApplication.ClientGrantType.Equals(GrantType.ResourceOwnerPassword))
            {
                _logger.LogError($"未找到对应的客户端授权类型，请检查客户端授权类型;client_id:{_validatedRequest.ClientApplication.ClientId}");
                return InvalidError(TokenErrors.UnauthorizedClient);
            }
            if (!(await ValidateRequestedScopesAsync(parameters)))
            {
                return InvalidError(TokenErrors.InvalidScope);
            }
            var userName = parameters.Get(TokenRequest.UserName);
            var passWord = parameters.Get(TokenRequest.Password);
            if(userName.IsNullOrEmpty())
            {
                return InvalidError(TokenErrors.InvalidGrant);
            }
            if(passWord.IsNullOrEmpty())
            {
                passWord = "";
            }
            _validatedRequest.UserName = userName;

            var resourceOwnerContext = new ResourceOwnerPasswordValidationContext
            {
                UserName = userName,
                PassWord = passWord,
                Request = _validatedRequest
            };
            // 用户名密码暂未验证，后补
            { 
            }
            // To Do 暂时不做任何校验，后补
            if(resourceOwnerContext.Result.IsError)
            {

            }
            if(resourceOwnerContext.Result.Subject==null)
            {

            }

            var isActiveContext = new IsActiveContext(resourceOwnerContext.Result.Subject,_validatedRequest.ClientApplication, ProfileIsActiveCallers.ResourceOwnerValidation);
            //Todo 少了一个方法判断
            if(isActiveContext.IsActive)
            {
                return InvalidError(TokenErrors.InvalidGrant);
            }

            _validatedRequest.UserName = userName;
            _validatedRequest.Subject = resourceOwnerContext.Result.Subject;
            return SuccessValid(resourceOwnerContext.Result.CustomResponse);
        }

        private async Task<bool> ValidateRequestedScopesAsync(NameValueCollection parameters)
        {
            await Task.CompletedTask;
            var scopes = parameters.Get(TokenRequest.Scope);
            if (scopes.IsNullOrEmpty())
            {

            }
            var requestScopesList = scopes.ParseScopesStringToList();
            if(requestScopesList is null)
            {
                _logger.LogError("请求中未找到Scope!");
                return false;
            }
            //To Do 暂时为判断请求的Scope先统一返回为true
            return true;
        }

        public TokenRequestValidationResult InvalidError(string error, string errorDescription = null, Dictionary<string, object> customResponse = null)
        {
            return new TokenRequestValidationResult(_validatedRequest, error: error, errorDescription: errorDescription, customResponse: customResponse);
        }
        private TokenRequestValidationResult SuccessValid(Dictionary<string, object> customResponse = null)
        {
            return new TokenRequestValidationResult(_validatedRequest,customResponse);
        }
    }
}

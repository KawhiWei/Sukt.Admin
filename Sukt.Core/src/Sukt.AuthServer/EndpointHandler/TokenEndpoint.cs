using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Sukt.AuthServer.Constants;
using Sukt.AuthServer.EndpointHandler.EndpointHandlerResult;
using Sukt.AuthServer.EndpointHandler.TokenError;
using Sukt.AuthServer.Extensions;
using Sukt.AuthServer.Generator;
using Sukt.AuthServer.Validation;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Sukt.Module.Core.OidcConstants;

namespace Sukt.AuthServer.EndpointHandler
{
    internal class TokenEndpoint : IEndpointHandler
    {
        private readonly IClientSecretValidator _clientSecretValidator;
        private readonly ITokenRequestValidator _tokenRequestValidator;
        private readonly ITokenResponseGenerator _tokenResponseGenerator;
        private readonly ILogger _logger;

        public TokenEndpoint(IClientSecretValidator clientSecretValidator, ILogger<TokenEndpoint> logger,
            ITokenRequestValidator tokenRequestValidator,
            ITokenResponseGenerator tokenResponseGenerator)
        {
            _clientSecretValidator = clientSecretValidator;
            _logger = logger;
            _tokenRequestValidator = tokenRequestValidator;
            _tokenResponseGenerator = tokenResponseGenerator;
        }

        public async Task<IEndpointResult> HandlerProcessAsync(HttpContext context)
        {
            _logger.LogDebug("Token请求处理器");
            if (!HttpMethods.IsPost(context.Request.Method) || !context.Request.HasApplicationFormContentType())
            {
                _logger.LogWarning("端点路由的HTTP请求无效！");
                return Error(TokenErrors.InvalidRequest);
            }
            return await ProcessTokenRequestAsync(context);
        }
        /// <summary>
        /// 处理获取Token请求
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task<IEndpointResult> ProcessTokenRequestAsync(HttpContext context)
        {
            _logger.LogDebug("开始处理Token请求");
            var clientResult = await _clientSecretValidator.ValidateAsync(context);
            if (clientResult.ClientApplication == null)
            {
                return Error(TokenErrors.InvalidClient, $"client_id: {clientResult.Secret?.Id}");
            }
            var form = (await context.Request.ReadFormAsync()).AsNameValueCollection();
            var requestResult = await _tokenRequestValidator.ValidateRequestAsync(form, clientResult);//验证传入进来的参数
            if (requestResult.IsError)
            {
                return Error(requestResult.Error, requestResult.ErrorDescription, requestResult.CustomResponse);
            }
            var tokenResponse = await _tokenResponseGenerator.ProcessAsync(requestResult);

            return new TokenErrorResult(new TokenErrorResponse());
            //await Task.CompletedTask;
        }
        private TokenErrorResult Error(string error, string errorDescription = null, Dictionary<string, object> custom = null)
        {
            var response = new TokenErrorResponse { Error = error, ErrorDescription = errorDescription, Custom = custom };
            return new TokenErrorResult(response);
        }
    }
}

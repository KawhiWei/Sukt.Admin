using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Sukt.AuthServer.Constants;
using Sukt.AuthServer.EndpointHandler.EndpointHandlerResult;
using Sukt.AuthServer.EndpointHandler.TokenError;
using Sukt.AuthServer.Extensions;
using Sukt.AuthServer.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.EndpointHandler
{
    internal class TokenEndpoint : IEndpointHandler
    {
        private readonly IClientSecretValidator _passwordClientSecretValidator;
        private readonly ILogger _logger;

        public TokenEndpoint(IClientSecretValidator passwordClientSecretValidator, ILogger<TokenEndpoint> logger)
        {
            _passwordClientSecretValidator = passwordClientSecretValidator;
            _logger = logger;
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

            await Task.CompletedTask;

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

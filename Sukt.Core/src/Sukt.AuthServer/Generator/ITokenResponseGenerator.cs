using Microsoft.AspNetCore.Http;
using Sukt.AuthServer.EndpointHandler.EndpointHandlerResult;
using Sukt.AuthServer.Validation.Response;
using Sukt.AuthServer.Validation.ValidationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Generator
{
    public interface ITokenResponseGenerator
    {
        /// <summary>
        /// 生成Token 处理器
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task<TokenResponse> ProcessAsync(TokenRequestValidationResult request);
    }
}

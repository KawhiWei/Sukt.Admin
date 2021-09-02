using Microsoft.AspNetCore.Http;
using Sukt.AuthServer.EndpointHandler.TokenError;
using Sukt.Module.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.EndpointHandler.EndpointHandlerResult
{
    public class TokenErrorResult : IEndpointResult
    {
        public TokenErrorResult(TokenErrorResponse response)
        {
            if (response.Error.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(response.Error), "没有错误信息写入");
            Response = response;
        }

        public TokenErrorResponse Response { get; }
        public Task ExecuteAsync(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }
}

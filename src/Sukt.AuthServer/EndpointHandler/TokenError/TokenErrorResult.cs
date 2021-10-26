using Microsoft.AspNetCore.Http;
using Sukt.AuthServer.EndpointHandler.TokenError;
using Sukt.AuthServer.Extensions;
using Sukt.Module.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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
        public async Task ExecuteAsync(HttpContext context)
        {
            context.Response.StatusCode = 400;
            context.Response.SetNoCache();
            var dto = new ResultDto
            {
                error = Response.Error,
                error_description = Response.ErrorDescription,

                custom = Response.Custom
            };
            await context.Response.WriteJsonAsync(dto);
        }
        internal class ResultDto
        {
            public string error { get; set; }
            public string error_description { get; set; }

            [JsonExtensionData]
            public Dictionary<string, object> custom { get; set; }
        }
    }
}

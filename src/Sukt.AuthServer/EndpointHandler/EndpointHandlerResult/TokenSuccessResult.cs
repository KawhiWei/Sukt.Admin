using Microsoft.AspNetCore.Http;
using Sukt.AuthServer.Extensions;
using Sukt.AuthServer.Validation.Response;
using Sukt.Module.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sukt.AuthServer.EndpointHandler.EndpointHandlerResult
{
    public class TokenSuccessResult : IEndpointResult
    {
        public TokenSuccessResult(TokenResponse response)
        {
            Response = response ?? throw new ArgumentNullException(nameof(response));
        }

        public TokenResponse Response { get; set; }
        public async Task ExecuteAsync(HttpContext context)
        {
            context.Response.SetNoCache();
            var dto = new ResultDto
            {
                id_token = Response.IdentityToken,
                access_token = Response.AccessToken,
                refresh_token = Response.RefreshToken,
                expires_in = Response.AccessTokenLifetime,
                token_type = OidcConstants.TokenResponse.BearerTokenType,
                scope = Response.Scope,
                Custom = Response.Custom
            };
            await context.Response.WriteJsonAsync(dto);
        }
        internal class ResultDto
        {
            public string id_token { get; set; }
            public string access_token { get; set; }
            public int expires_in { get; set; }
            public string token_type { get; set; }
            public string refresh_token { get; set; }
            public string scope { get; set; }
            [JsonExtensionData]
            public Dictionary<string, object> Custom { get; set; }
        }
    }
    
}

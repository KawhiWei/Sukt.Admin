using Microsoft.AspNetCore.Http;
using Sukt.AuthServer.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.EndpointHandler.EndpointHandlerResult
{
    public class JsonWebKeysResult : IEndpointResult
    {
        public JsonWebKeysResult(IEnumerable<SuktJsonWebKey> suktJsonWebKey)
        {
            SuktJsonWebKey = suktJsonWebKey;
        }

        public IEnumerable<SuktJsonWebKey> SuktJsonWebKey { get; set; }
        public Task ExecuteAsync(HttpContext context)
        {
            return context.Response.WriteJsonAsync(new { keys = SuktJsonWebKey }, "application/json; charset=UTF-8");
        }
    }
}

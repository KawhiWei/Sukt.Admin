using Microsoft.AspNetCore.Http;
using Sukt.AuthServer.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.EndpointHandler.EndpointHandlerResult
{
    public class DiscoveryResult : IEndpointResult
    {
        public DiscoveryResult(Dictionary<string, object> entities)
        {
            Entities = entities;
        }

        public Dictionary<string, object> Entities { get; set; } = new Dictionary<string, object>();

        public Task ExecuteAsync(HttpContext context)
        {
            return context.Response.WriteJsonAsync(Entities);
        }
    }
}

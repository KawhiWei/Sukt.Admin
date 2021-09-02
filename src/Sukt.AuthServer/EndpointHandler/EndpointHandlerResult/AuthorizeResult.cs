using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.EndpointHandler.EndpointHandlerResult
{
    public class AuthorizeResult : IEndpointResult
    {
        public async Task ExecuteAsync(HttpContext context)
        {
            await Task.CompletedTask;
        }
    }
}

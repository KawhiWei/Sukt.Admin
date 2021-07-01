using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.EndpointHandler.EndpointHandlerResult
{
    public interface IEndpointResult
    {
        Task ExecuteAsync(HttpContext context);
    }
}

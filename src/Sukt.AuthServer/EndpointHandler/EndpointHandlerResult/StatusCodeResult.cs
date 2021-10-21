using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.EndpointHandler.EndpointHandlerResult
{
    public class StatusCodeResult : IEndpointResult
    {
        public StatusCodeResult(HttpStatusCode statusCode)
        {
            StatusCode = (int)statusCode;
        }
        public int StatusCode { get; }
        public Task ExecuteAsync(HttpContext context)
        {
            context.Response.StatusCode = StatusCode;

            return Task.CompletedTask;
        }
    }
}

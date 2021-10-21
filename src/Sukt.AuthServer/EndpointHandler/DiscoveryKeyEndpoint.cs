using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Sukt.AuthServer.EndpointHandler.EndpointHandlerResult;
using Sukt.AuthServer.Generator.DiscoveryDocument;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.EndpointHandler
{
    public class DiscoveryKeyEndpoint : IEndpointHandler
    {
        private readonly ILogger _logger;
        private readonly IDiscoveryDocument _discoveryDocument;

        public DiscoveryKeyEndpoint(ILogger<DiscoveryKeyEndpoint> logger, IDiscoveryDocument discoveryDocument)
        {
            _logger = logger;
            _discoveryDocument = discoveryDocument;
        }

        public async Task<IEndpointResult> HandlerProcessAsync(HttpContext context)
        {
            _logger.LogInformation("开始处理获取Jwk私钥!");
            if (!HttpMethods.IsGet(context.Request.Method))
            {
                return new StatusCodeResult(System.Net.HttpStatusCode.MethodNotAllowed);
            }
            var responese=await _discoveryDocument.GetJwkDocumentAsync();
            _logger.LogInformation("私钥获取成功，正在返回给资源Api!");
            return new JsonWebKeysResult(responese);
        }
    }
}

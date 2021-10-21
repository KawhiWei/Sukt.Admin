using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Sukt.AuthServer.EndpointHandler;
using Sukt.AuthServer.EndpointHandler.EndpointHandlerResult;
using Sukt.AuthServer.Extensions;
using Sukt.AuthServer.Generator;
using Sukt.AuthServer.Generator.DiscoveryDocument;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer
{
    /// <summary>
    /// 请求文档端点路由
    /// </summary>
    public class DiscoveryEndpoint : IEndpointHandler
    {
        private readonly ILogger _logger;
        private readonly IDiscoveryDocument _discoveryDocument;


        public DiscoveryEndpoint(ILogger<DiscoveryEndpoint> logger, IDiscoveryDocument discoveryDocument)
        {
            _logger = logger;
            _discoveryDocument = discoveryDocument;
        }

        public async Task<IEndpointResult> HandlerProcessAsync(HttpContext context)
        {
            _logger.LogInformation($"开始请求文档!");
            var baseurl=context.GetSuktAuthServerBaseUrl().EnsureTrailingSlash();
            var issuerurl = context.GetSuktAuthServerBaseUrl();
            var responses =await  _discoveryDocument.GetDocumentsAsync(baseurl, issuerurl);
            _logger.LogInformation("SuktAuthServer 文档请求成功,正在返回给客户端~");
            return new DiscoveryResult(responses);
        }
    }
}

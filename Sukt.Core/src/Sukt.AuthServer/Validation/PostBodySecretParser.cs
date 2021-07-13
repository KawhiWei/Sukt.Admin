using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Sukt.AuthServer.Constants;
using Sukt.AuthServer.Domain.Models;
using Sukt.AuthServer.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Validation
{
    /// <summary>
    /// Post参数解析
    /// </summary>
    public class PostBodySecretParser : ISecretParser
    {
        private readonly ILogger _logger;

        public PostBodySecretParser(ILogger<PostBodySecretParser> logger)
        {
            _logger = logger;
        }

        public string AuthenticationMethod => EndpointAuthenticationMethods.PostBody;

        public async Task<ParsedSecret> ParseAsync(HttpContext context)
        {
            _logger.LogDebug("开始解析Post请求中的数据");
            if (!context.Request.HasApplicationFormContentType())
            {
                _logger.LogDebug("请求类型未找到");
                return null;
            }
            var body = await context.Request.ReadFormAsync();
            if (body != null)
            {
                var id = body["client_id"].FirstOrDefault();
                var secret = body["client_secret"].FirstOrDefault();
                if (id.IsPresent())
                {
                    if (secret.IsPresent())
                    {
                        return new ParsedSecret
                        {
                            Id = id,
                            Credential = secret,
                            Type = ParsedSecretTypes.SharedSecret
                        };
                    }
                    else
                    {
                        // client secret is optional
                        _logger.LogDebug("client id without secret found");
                        return new ParsedSecret
                        {
                            Id = id,
                            Type = ParsedSecretTypes.NoSecret
                        };
                    }
                }
            }
            _logger.LogDebug("未找到Post请求中的密钥！");
            return null;
        }
        
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Sukt.AuthServer.Constants;
using Sukt.AuthServer.Domain.Models;
using Sukt.AuthServer.Domain.SuktAuthServer.SuktApplicationStore;
using Sukt.AuthServer.Validation.SecretValidates;
using Sukt.AuthServer.Validation.ValidationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Validation
{
    public class ClientSecretValidator : IClientSecretValidator
    {
        private readonly ILogger _logger;
        private readonly ISuktApplicationStore _suktApplicationStore;
        private readonly IEnumerable<ISecretParser> _parsers;
        private readonly IEnumerable<ISecretValidator> _secretValidators;
        public ClientSecretValidator(ILogger<ClientSecretValidator> logger, ISuktApplicationStore suktApplicationStore, IEnumerable<ISecretParser> parsers, IEnumerable<ISecretValidator> secretValidators)
        {
            _logger = logger;
            _suktApplicationStore = suktApplicationStore;
            _parsers = parsers;
            _secretValidators = secretValidators;
        }
        /// <summary>
        /// 客户端验证处理器
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<ClientSecretValidationResult> ValidateAsync(HttpContext context)
        {
            _logger.LogDebug("开始处理客户端验证！");
            var resultfail = new ClientSecretValidationResult { IsError = true };
            ParsedSecret parsedSecret = null;
            foreach (var parser in _parsers)
            {
                parsedSecret = await parser.ParseAsync(context);
                if (parsedSecret != null)
                {
                    _logger.LogDebug("找到了密钥: {type}", parser.GetType().Name);
                    if (parsedSecret.Type != ParsedSecretTypes.NoSecret)
                    {
                        break;
                    }
                }
            }
            resultfail.Secret = parsedSecret;
            if (parsedSecret == null)
            {
                _logger.LogDebug("未找到客户端标识符: {id}！", parsedSecret.Id);
                return resultfail;
            }
            var client=await _suktApplicationStore.FindByClientIdAsync(parsedSecret.Id);
            if(client==null)
            {
                _logger.LogError("未找到客户端应用 '{clientId}'", parsedSecret.Id);
                return resultfail;
            }
            //if (!client.RequireClientSecret || client.IsImplicitOnly()) //判断是否是公共密钥
            //{
            //    _logger.LogDebug("Public Client - skipping secret validation success");
            //}
            SecretValidationResult secretValidationResult = null;
            foreach (var secretValidator in _secretValidators)
            {
                secretValidationResult = await secretValidator.ValidateAsync(client, parsedSecret);
                if(secretValidationResult.Success)
                {
                    break;
                }
            }
            if(!secretValidationResult.Success)
            {
                //to do 失败事件等等 未写
                _logger.LogError("");
            }


            var success = new ClientSecretValidationResult
            {
                IsError = false,
                ClientApplication = client,
                Secret = parsedSecret,
                Confirmation = secretValidationResult?.Confirmation
            };
            // to do  成功事件未写
            return success;
        }
    }
}

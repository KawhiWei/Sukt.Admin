using Microsoft.Extensions.Logging;
using Sukt.AuthServer.Domain.Models;
using Sukt.AuthServer.Domain.SuktAuthServer;
using Sukt.AuthServer.Validation.ValidationResult;
using Sukt.Core.Domain.Models.IdentityServerFour;
using Sukt.Module.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Validation
{
    /// <summary>
    /// 资源认证服务实现
    /// </summary>
    public class DefaultResourceValidator: IResourceValidator
    {
        private readonly ILogger _logger;
        private readonly ISuktResourceScopeStore _suktResourceScopeStore;

        public DefaultResourceValidator(ILogger<DefaultResourceValidator> logger, ISuktResourceScopeStore suktResourceScopeStore)
        {
            _logger = logger;
            _suktResourceScopeStore = suktResourceScopeStore;
        }
        /// <summary>
        /// 验证配置的资源作用域
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual async Task<ResourceValidationResult> ValidateRequestedResourcesAsync(ResourceValidationRequest request)
        {
            if(request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            if (request.Scopes is null)
            {
                throw new ArgumentNullException(nameof(request.Scopes));
            }
            var result = new ResourceValidationResult();
            var ResouceStoreResult = await _suktResourceScopeStore.FindResourcesByScopeAsync(request.Scopes);

            foreach (var scope in request.Scopes)
            {
                await ValidateScopeAsync(request.ClientApplication, ResouceStoreResult, scope, result);
            }
            if(result.InvalidScopes.Count>0)
            {
                result.ParsedScopes.Clear();
                result.SuktResources.SuktResources.Clear();
            }
            return result;
        }
        /// <summary>
        /// 验证客户端请求过来的作用域
        /// </summary>
        /// <param name="suktApplication"></param>
        /// <param name="suktResource"></param>
        /// <param name="scopeName"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        protected virtual async Task ValidateScopeAsync(SuktApplicationModel suktApplication,SuktResource suktResource,string scopeName, ResourceValidationResult result)
        {
            //var resourcescope=  from scope in suktResource.SuktResources
            //                    where scope.Resources.Contains(scopeName)
            var resourcescope = suktResource.SuktResources.FirstOrDefault(x => x.Resources.Contains(scopeName));
            if(resourcescope!=null)
            {
                var scope = resourcescope.Resources.FirstOrDefault(x => x.Equals(scopeName));
                if(!scope.IsNullOrEmpty())
                {
                    if (await IsClientAllowedIdentityResourceAsync(suktApplication, scope))
                    {
                        result.ParsedScopes.Add(scope);
                        result.SuktResources.SuktResources.Add(resourcescope);
                    }
                    else
                    {
                        result.InvalidScopes.Add(scopeName);
                    }
                }
            }
        }
        protected virtual Task<bool> IsClientAllowedIdentityResourceAsync(SuktApplicationModel suktApplication, string scope)
        {
            var allowed = suktApplication.ClientScopes.Contains(scope);
            if (!allowed)
            {
                _logger.LogError("客户端 {client} 是否允许访问的范围 {scope}.", suktApplication.ClientId, scope);
            }
            return Task.FromResult(allowed);
        }
    }
}

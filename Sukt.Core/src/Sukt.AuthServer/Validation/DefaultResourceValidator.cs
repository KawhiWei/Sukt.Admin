using Microsoft.Extensions.Logging;
using Sukt.AuthServer.Domain.Models;
using Sukt.AuthServer.Domain.SuktAuthServer;
using Sukt.AuthServer.Validation.ValidationResult;
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





            return result;

        }
        protected virtual async Task ValidateScopeAsync(SuktApplicationModel suktApplicationModel,SuktResource suktResource,List<string> scopes,ResourceValidationRequest result)
        {


        }
    }
}

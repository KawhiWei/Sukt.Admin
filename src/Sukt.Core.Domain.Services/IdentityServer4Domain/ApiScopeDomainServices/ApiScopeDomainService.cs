using Sukt.Core.Domain.Models.IdentityServerFour;
using Sukt.Module.Core;
using Sukt.Module.Core.OperationResult;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sukt.Core.Domain.Services.IdentityServer4Domain.ApiScopeDomainServices
{
    public class ApiScopeDomainService : IApiScopeDomainService
    {
        private readonly IAggregateRootRepository<ApiScope, Guid> _apiScopeRepository=null;

        public ApiScopeDomainService(IAggregateRootRepository<ApiScope, Guid> apiScopeRepository)
        {
            _apiScopeRepository = apiScopeRepository;
        }

        public async Task<OperationResponse> CreateAsync(List<ApiScope> apiScopes)
        {
            return await _apiScopeRepository.InsertAsync(apiScopes.ToArray());
        }
    }
}

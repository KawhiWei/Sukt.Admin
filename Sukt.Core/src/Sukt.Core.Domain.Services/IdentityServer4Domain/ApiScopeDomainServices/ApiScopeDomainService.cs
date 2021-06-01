using Sukt.Core.Domain.Models.IdentityServerFour;
using SuktCore.Shared;
using SuktCore.Shared.OperationResult;
using System;
using System.Threading.Tasks;

namespace Sukt.Core.Domain.Services.IdentityServer4Domain.ApiScopeDomainServices
{
    public class ApiScopeDomainService : IApiScopeDomainService
    {
        private readonly IAggregateRootRepository<ApiScope, Guid> _apiScopeRepository;
        public async Task<OperationResponse> CreateAsync(ApiScope[] apiScopes)
        {
            return await _apiScopeRepository.InsertAsync(apiScopes);
        }
    }
}

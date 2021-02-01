using Sukt.Core.Domain.Models.IdentityServerFour;
using Sukt.Core.Shared;
using Sukt.Core.Shared.OperationResult;
using System;
using System.Threading.Tasks;

namespace Sukt.Core.Domain.Services.IdentityServer4Domain.ApiResourceDomainServices
{
    public class ApiResourceDomainService : IApiResourceDomainService
    {
        private readonly IAggregateRootRepository<ApiResource, Guid> _apiResourceRepository;

        public ApiResourceDomainService(IAggregateRootRepository<ApiResource, Guid> apiResourceRepository)
        {
            _apiResourceRepository = apiResourceRepository;
        }
        public async Task<OperationResponse> CreateApiResourceAsync(ApiResource apiResource)
        {
            return await _apiResourceRepository.InsertAsync(apiResource);
        }
        public async Task<ApiResource> GetLoadAsync(Guid id)
        {
            return await _apiResourceRepository.GetByIdAsync(id);
        }
        public async Task<OperationResponse> UpdateAsync(ApiResource apiResource)
        {
            return await _apiResourceRepository.UpdateAsync(apiResource);
        }

    }
}

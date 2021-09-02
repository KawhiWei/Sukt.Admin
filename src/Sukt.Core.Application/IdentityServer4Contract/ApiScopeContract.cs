using Sukt.Core.Domain.Models.IdentityServerFour;
using Sukt.Core.Domain.Services.IdentityServer4Domain.ApiScopeDomainServices;
using Sukt.Core.Dtos.IdentityServer4Dto.ApiScope;
using Sukt.Module.Core.Extensions;
using Sukt.Module.Core.OperationResult;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sukt.Core.Application.IdentityServer4Contract
{
    public class ApiScopeContract : IApiScopeContract
    {
        private readonly IApiScopeDomainService _apiScopeDomainService;
        public ApiScopeContract(IApiScopeDomainService apiScopeDomainService)
        {
            _apiScopeDomainService = apiScopeDomainService;
        }

        public async Task<OperationResponse> CreateAsync(List<ApiScopeInputDto> input)
        {
            input.NotNull(nameof(input));
            return await _apiScopeDomainService.CreateAsync(input.Select(x => new ApiScope(x.Name, x.DisplayName)).ToList());
            //return await _apiScopeDomainService.CreateAsync(input.Select(x => new ApiScope()).ToArray());
        }
    }
}

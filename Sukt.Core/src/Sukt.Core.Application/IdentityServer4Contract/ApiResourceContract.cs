using Microsoft.AspNetCore.Mvc.Rendering;
using Sukt.Core.Domain.Models.IdentityServerFour;
using Sukt.Core.Domain.Services.IdentityServer4Domain.ApiResourceDomainServices;
using Sukt.Core.Dtos.IdentityServer4Dto;
using Sukt.Core.Dtos.IdentityServer4Dto.ApiResource;
using Sukt.Module.Core.OperationResult;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sukt.Module.Core.Extensions;
using Sukt.Module.Core;

namespace Sukt.Core.Application.IdentityServer4Contract
{
    public class ApiResourceContract : IApiResourceContract
    {
        private readonly IApiResourceDomainService _apiResourceDomainService;
        public ApiResourceContract(IApiResourceDomainService apiResourceDomainService)
        {
            _apiResourceDomainService = apiResourceDomainService;
        }
        public async Task<OperationResponse> CreateAsync(ApiResourceInputDto input)
        {
            input.NotNull(nameof(input));
            var apiresourceentity = new ApiResource(input.Name, input.DisplayName);
            //var apiresourceentity = new ApiResource();
            apiresourceentity.AddUserClaims(input.UserClaims);
            return await _apiResourceDomainService.CreateApiResourceAsync(apiresourceentity);
        }
        public async Task<OperationResponse> CreateApiResourceSecretAsync(SecretInputDto input)
        {
            input.NotNull(nameof(input));
            var entity = await _apiResourceDomainService.GetLoadAsync(input.Id);
            entity.AddSecrets(new ApiResourceSecret(input.Value, input.Type, null));
            return await _apiResourceDomainService.UpdateAsync(entity);
        }
        public async Task<OperationResponse> CreateApiResourceScopesAsync(CommonInputDto input)
        {
            input.NotNull(nameof(input));
            var entity = await _apiResourceDomainService.GetLoadAsync(input.Id);
            entity.AddScopes(input.Allowed);
            return await _apiResourceDomainService.UpdateAsync(entity);
        }
        public OperationResponse GetJwtClaimTypeSelectItem()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            var type = typeof(JwtClaimTypes);
            var items = type.GetFields().Select(o => new SelectListItem { Text = o.Name, Value = o.GetValue(type)?.ToString() });
            return OperationResponse.Ok("得到数据", items);
        }
    }
}

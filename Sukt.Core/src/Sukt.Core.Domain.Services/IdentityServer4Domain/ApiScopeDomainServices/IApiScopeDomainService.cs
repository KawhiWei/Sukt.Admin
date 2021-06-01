using Sukt.Core.Domain.Models.IdentityServerFour;
using SuktCore.Shared;
using SuktCore.Shared.OperationResult;
using System.Threading.Tasks;

namespace Sukt.Core.Domain.Services.IdentityServer4Domain.ApiScopeDomainServices
{
    public interface IApiScopeDomainService : IScopedDependency
    {
        /// <summary>
        /// 添加授权范围
        /// </summary>
        /// <param name="apiScopes"></param>
        /// <returns></returns>
        Task<OperationResponse> CreateAsync(ApiScope[] apiScopes);
    }
}

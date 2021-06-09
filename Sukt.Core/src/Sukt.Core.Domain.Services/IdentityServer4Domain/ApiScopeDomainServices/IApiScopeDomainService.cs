using Sukt.Core.Domain.Models.IdentityServerFour;
using Sukt.Module.Core.OperationResult;
using System.Threading.Tasks;
using Sukt.Module.Core;
using System.Collections.Generic;

namespace Sukt.Core.Domain.Services.IdentityServer4Domain.ApiScopeDomainServices
{
    public interface IApiScopeDomainService : IScopedDependency
    {
        /// <summary>
        /// 添加授权范围
        /// </summary>
        /// <param name="apiScopes"></param>
        /// <returns></returns>
        Task<OperationResponse> CreateAsync(List<ApiScope> apiScopes);
    }
}

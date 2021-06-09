using Sukt.Core.Dtos.IdentityServer4Dto.ApiScope;
using Sukt.Module.Core;
using Sukt.Module.Core.OperationResult;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sukt.Core.Application.IdentityServer4Contract
{
    public interface IApiScopeContract : IScopedDependency
    {
        /// <summary>
        /// 添加Api授权访问范围
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OperationResponse> CreateAsync(List<ApiScopeInputDto> input);
    }
}

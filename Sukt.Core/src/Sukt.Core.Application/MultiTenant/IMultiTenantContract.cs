using Sukt.Core.Dtos.MultiTenant;
using SuktCore.Shared;
using SuktCore.Shared.Entity;
using SuktCore.Shared.Extensions.ResultExtensions;
using SuktCore.Shared.OperationResult;
using System;
using System.Threading.Tasks;

namespace Sukt.Core.Application.MultiTenant
{
    public interface IMultiTenantContract : IScopedDependency
    {
        /// <summary>
        /// 创建租户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OperationResponse> CreatAsync(MultiTenantInputDto input);
        /// <summary>
        /// 修改租户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OperationResponse> UpdateAsync(MultiTenantInputDto input);
        /// <summary>
        /// 加载租户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<OperationResponse> LoadAsync(Guid id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IPageResult<MultiTenantOutPutPageDto>> GetLoadPageAsync(PageRequest request);

    }
}

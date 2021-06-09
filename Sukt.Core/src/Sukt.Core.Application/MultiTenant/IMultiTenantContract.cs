using Sukt.Core.Dtos.MultiTenant;
using Sukt.Module.Core;
using Sukt.Module.Core.Entity;
using Sukt.Module.Core.OperationResult;
using System;
using System.Threading.Tasks;
using Sukt.Module.Core.Extensions.ResultExtensions;

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

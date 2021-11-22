
using Sukt.Module.Core;
using Sukt.Module.Core.Entity;
using Sukt.Module.Core.OperationResult;
using System;
using System.Threading.Tasks;
using Sukt.Module.Core.Extensions.ResultExtensions;
using Sukt.Core.Dtos.Tenant;

namespace Sukt.Core.Application.Tenant
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
        Task<OperationResponse> UpdateAsync(Guid id, MultiTenantInputDto input);
        /// <summary>
        /// 加载租户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<OperationResponse> LoadFormAsync(Guid id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IPageResult<MultiTenantOutPutPageDto>> GetPageAsync(PageRequest request);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<OperationResponse> DeleteAsync(Guid id);

    }
}

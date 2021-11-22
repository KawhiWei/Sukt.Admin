using Sukt.Module.Core;
using Sukt.Module.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.Core.Application.Tenant
{
    public interface IMultiTenantConntionStringContract : IScopedDependency
    {
        /// <summary>
        /// 创建租户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OperationResponse> CreatAsync(MultiTenantConntionStringInputDto input);
        /// <summary>
        /// 修改租户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OperationResponse> UpdateAsync(Guid id, MultiTenantConntionStringInputDto input);
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

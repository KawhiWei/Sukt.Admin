
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
        Task<OperationResponse> CreateAsync(MultiTenantInputDto input);
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
        /// <summary>
        /// 添加数据库连接字符串
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OperationResponse> CreateAsync(Guid tenantId, MultiTenantConnectionStringInputDto input);
        /// <summary>
        /// 添加数据库连接字符串
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OperationResponse> UpdateAsync(Guid tenantId,Guid id, MultiTenantConnectionStringInputDto input);
        /// <summary>
        /// 删除连接字符串
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<OperationResponse> DeleteAsync(Guid tenantId, Guid id);
        /// <summary>
        /// 加载连接字符串表单
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<OperationResponse> LoadFormAsync(Guid tenantId, Guid id);
        /// <summary>
        /// 分页获取租户连接字符串
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IPageResult<MultiTenantConnectionStringOutPutDto>> GetPageAsync(Guid tenantId,PageRequest request);

    }
}

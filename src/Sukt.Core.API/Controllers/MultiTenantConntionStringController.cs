using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sukt.Core.Application.Tenant;
using Sukt.Core.Dtos.Tenant;
using Sukt.Core.Shared;
using Sukt.Module.Core.AjaxResult;
using Sukt.Module.Core.Audit;
using Sukt.Module.Core.Entity;
using Sukt.Module.Core.Extensions;
using Sukt.Module.Core.OperationResult;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Sukt.Core.API.Controllers
{
    /// <summary>
    /// 租户连接字符串管理
    /// </summary>
    [Description("租户连接字符串管理")]
    public class MultiTenantConntionStringController : ApiControllerBase
    {
        private readonly IMultiTenantContract _multiTenantContract;

        public MultiTenantConntionStringController(IMultiTenantContract multiTenantContract)
        {
            _multiTenantContract = multiTenantContract;
        }
        /// <summary>
        /// 添加数据库连接字符串
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("{tenantId}")]
        [Description("添加数据库连接字符串")]
        [AuditLog]
        public async Task<AjaxResult> CreateAsync(Guid tenantId, [FromBody] MultiTenantConnectionStringInputDto input)
        {
            return (await _multiTenantContract.CreateAsync(tenantId, input)).ToAjaxResult();
        }
        /// <summary>
        /// 修改数据库连接字符串
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{tenantId}/{id}")]
        [Description("修改数据库连接字符串")]
        [AuditLog]
        public async Task<AjaxResult> UpdateAsync(Guid tenantId, Guid id, [FromBody] MultiTenantConnectionStringInputDto input)
        {
            return (await _multiTenantContract.UpdateAsync(tenantId, id, input)).ToAjaxResult();
        }
        /// <summary>
        /// 删除连接字符串
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{tenantId}/{id}")]
        [Description("删除连接字符串")]
        [AuditLog]
        public async Task<AjaxResult> DeleteAsync(Guid tenantId, Guid id)
        {
            return (await _multiTenantContract.DeleteAsync(tenantId, id)).ToAjaxResult();
        }
        /// <summary>
        /// 加载租户连接字符串
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{tenantId}/{id}")]
        [Description("加载租户连接字符串")]
        [AuditLog]
        public async Task<AjaxResult> LoadFormAsync(Guid tenantId, Guid id)
        {
            return (await _multiTenantContract.DeleteAsync(tenantId, id)).ToAjaxResult();
        }
        /// <summary>
        /// 分页获取租户连接字符串
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{tenantId}")]
        [Description("分页获取租户连接字符串")]
        public async Task<PageList<MultiTenantConnectionStringOutPutDto>> GetPageAsync(Guid tenantId,[FromBody] PageRequest request)
        {
            return (await _multiTenantContract.GetPageAsync(tenantId,request)).PageList();
        }
    }
}

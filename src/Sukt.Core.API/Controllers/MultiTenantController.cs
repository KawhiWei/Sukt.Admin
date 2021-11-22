using Microsoft.AspNetCore.Mvc;
using Sukt.Core.Shared;
using Sukt.Core.Dtos.Tenant;
using Sukt.Module.Core.AjaxResult;
using Sukt.Module.Core.Audit;
using Sukt.Module.Core.Entity;
using Sukt.Module.Core.Extensions;
using Sukt.Module.Core.OperationResult;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Sukt.Core.Application.Tenant;

namespace Sukt.Core.API.Controllers
{
    /// <summary>
    /// 租户管理
    /// </summary>
    [Description("租户管理")]
    public class MultiTenantController : ApiControllerBase
    {
        private readonly IMultiTenantContract _multiTenantContract;

        public MultiTenantController(IMultiTenantContract multiTenantContract)
        {
            _multiTenantContract = multiTenantContract;
        }

        /// <summary>
        /// 创建租户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("创建租户")]
        [AuditLog]
        public async Task<AjaxResult> CreateAsync([FromBody] MultiTenantInputDto input)
        {
            return (await _multiTenantContract.CreatAsync(input)).ToAjaxResult();
        }
        /// <summary>
        /// 修改租户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Description("修改租户")]
        [AuditLog]
        public async Task<AjaxResult> UpdateAsync(Guid id,[FromBody] MultiTenantInputDto input)
        {
            return (await _multiTenantContract.UpdateAsync(id,input)).ToAjaxResult();
        }
        /// <summary>
        /// 加载租户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Description("加载租户")]
        public async Task<AjaxResult> LoadFormAsync(Guid id)
        {
            return (await _multiTenantContract.LoadFormAsync(id)).ToAjaxResult();
        }
        /// <summary>
        /// 分页获获取租户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("分页获获取租户")]
        public async Task<PageList<MultiTenantOutPutPageDto>> GetPageAsync([FromBody] PageRequest request)
        {
            return (await _multiTenantContract.GetPageAsync(request)).PageList();
        }
        /// <summary>
        /// 删除租户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Description("删除租户")]
        [AuditLog]
        public async Task<AjaxResult> DeleteAsync(Guid id)
        {
            return (await _multiTenantContract.DeleteAsync(id)).ToAjaxResult();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Sukt.Core.Application.MultiTenant;
using SuktCore.AspNetCore.ApiBase;
using Sukt.Core.Dtos.MultiTenant;
using SuktCore.Shared.AjaxResult;
using SuktCore.Shared.Audit;
using SuktCore.Shared.Entity;
using SuktCore.Shared.Extensions;
using SuktCore.Shared.OperationResult;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Sukt.Core.API.Controllers
{
    /// <summary>
    /// 功能管理
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
        [Description("创建菜单")]
        [AuditLog]
        public async Task<AjaxResult> CreateAsync([FromBody] MultiTenantInputDto input)
        {
            return (await _multiTenantContract.CreatAsync(input)).ToAjaxResult();
        }
        /// <summary>
        /// 修改租户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Description("修改租户")]
        [AuditLog]
        public async Task<AjaxResult> UpdateAsync([FromBody] MultiTenantInputDto input)
        {
            return (await _multiTenantContract.UpdateAsync(input)).ToAjaxResult();
        }
        /// <summary>
        /// 加载租户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Description("加载租户")]
        public async Task<AjaxResult> LoadAsync(Guid? id)
        {
            return (await _multiTenantContract.LoadAsync(id.Value)).ToAjaxResult();
        }
        /// <summary>
        /// 分页获获取租户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("分页获获取租户")]
        public async Task<PageList<MultiTenantOutPutPageDto>> GetLoadPageAsync([FromBody] PageRequest request)
        {
            return (await _multiTenantContract.GetLoadPageAsync(request)).PageList();
        }
    }
}

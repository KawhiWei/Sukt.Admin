using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sukt.Core.Application.Identity.Role;
using Sukt.Core.Shared;
using Sukt.Core.Dtos.Identity.Role;
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
    /// 角色管理
    /// </summary>
    [Description("角色管理")]
    public class RoleController : ApiControllerBase
    {
        private readonly IRoleContract _roleContract = null;
        private readonly ILogger<RoleController> _logger = null;

        public RoleController(IRoleContract roleContract, ILogger<RoleController> logger)
        {
            _roleContract = roleContract;
            _logger = logger;
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("添加角色")]
        [AuditLog]
        public async Task<AjaxResult> CreateAsync([FromBody] RoleInputDto input)
        {
            return (await _roleContract.CreateAsync(input)).ToAjaxResult();
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="input"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Description("修改角色")]
        [AuditLog]
        public async Task<AjaxResult> UpdateAsync(Guid id,[FromBody] RoleInputDto input)
        {
            return (await _roleContract.UpdateAsync(id, input)).ToAjaxResult();
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Description("删除角色")]
        [AuditLog]
        public async Task<AjaxResult> DeleteAsync(Guid id)
        {
            return (await _roleContract.DeleteAsync(id)).ToAjaxResult();
        }
        /// <summary>
        /// 分页获取角色
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("分页获取角色")]
        public async Task<PageList<RoleOutPutPageDto>> GetPageAsync([FromBody] PageRequest request)
        {
            return (await _roleContract.GetPageAsync(request)).PageList();
        }
        /// <summary>
        /// 角色分配权限
        /// </summary>
        /// <returns></returns>
        [Description("角色分配权限")]
        [HttpPost]
        public async Task<AjaxResult> AllocationRoleMenuAsync([FromBody] RoleMenuInputDto dto)
        {
            return (await _roleContract.AllocationRoleMenuAsync(dto)).ToAjaxResult();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sukt.Core.Application.Identity.RoleMenu;
using Sukt.Core.Shared;
using Sukt.Module.Core.Audit;
using Sukt.Module.Core.OperationResult;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Sukt.Core.API.Controllers
{
    public class RoleMenuController : ApiControllerBase
    {
        private readonly IRoleMenuContract _roleMenuContract;
        public RoleMenuController(IRoleMenuContract roleMenuContract)
        {
            _roleMenuContract = roleMenuContract;
        }
        /// <summary>
        /// 角色分配菜单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="menuids"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        [Description("角色分配菜单")]
        [AuditLog]
        public async Task<AjaxResult>  AllocationMenuAsync(Guid id, Guid[] menuids)
        {
            return (await _roleMenuContract.AllocationRoleAsync(id, menuids)).ToAjaxResult();
        }
        /// <summary>
        /// 获取角色菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Description("获取角色菜单")]
        public async Task<AjaxResult> GetAllocationRoleMenuIdAsync(Guid id)
        {
            return (await _roleMenuContract.GetAllocationRoleMenuIdAsync(id)).ToAjaxResult();
        }
    }
}

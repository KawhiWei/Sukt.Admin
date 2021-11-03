using Microsoft.AspNetCore.Mvc;
using Sukt.Core.Application.Identity.UserRole;
using Sukt.Core.Shared;
using Sukt.Core.Dtos.Identity.UserRole;
using Sukt.Module.Core.Audit;
using Sukt.Module.Core.OperationResult;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Sukt.Core.API.Controllers
{
    [Description("用户角色管理")]
    public class UserRoleController : ApiControllerBase
    {
        private readonly IUserRoleContract _userRoleContract;

        public UserRoleController(IUserRoleContract userRoleContract)
        {
            _userRoleContract = userRoleContract;
        }
        /// <summary>
        /// 用户分配角色
        /// </summary>
        /// <param name="id"></param>
        /// <param name="roleids"></param>
        /// <returns></returns>
        [Description("用户分配角色")]
        [HttpPost("{id}")]
        [AuditLog]
        public async Task<AjaxResult> AllocationUserRoleAsync(Guid id, [FromBody] Guid[] roleids)
        {
            return (await _userRoleContract.AllocationRoleAsync(id, roleids)).ToAjaxResult();
        }
        /// <summary>
        /// 获取用户角色
        /// </summary>
        /// <returns></returns>
        [Description("获取用户角色")]
        [HttpGet("{id}")]
        public async Task<AjaxResult> GetLoadUserRoleAsync(Guid id)
        {
            return (await _userRoleContract.GetLoadUserRoleAsync(id)).ToAjaxResult();
        }
    }
}

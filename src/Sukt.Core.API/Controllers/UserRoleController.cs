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
        /// <returns></returns>
        [Description("用户分配角色")]
        [HttpPost]
        [AuditLog]
        public async Task<AjaxResult> AllocationUserRoleAsync([FromBody] UserRoleInputDto dto)
        {
            return (await _userRoleContract.AllocationRoleAsync(dto)).ToAjaxResult();
        }
        /// <summary>
        /// 获取用户角色
        /// </summary>
        /// <returns></returns>
        [Description("获取用户角色")]
        [HttpGet]
        public async Task<AjaxResult> GetLoadUserRoleAsync([FromQuery] Guid? id)
        {
            return (await _userRoleContract.GetLoadUserRoleAsync(id.Value)).ToAjaxResult();
        }
    }
}

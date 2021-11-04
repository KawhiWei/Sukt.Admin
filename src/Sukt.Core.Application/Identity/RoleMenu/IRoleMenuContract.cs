using Sukt.Module.Core;
using Sukt.Module.Core.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.Core.Application.Identity.RoleMenu
{
    public interface IRoleMenuContract : IScopedDependency
    {
        /// <summary>
        /// 角色分配菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="roleids"></param>
        /// <returns></returns>
        Task<OperationResponse> AllocationRoleAsync(Guid roleId, Guid[] roleids);
        /// <summary>
        /// 获取角色菜单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<OperationResponse> GetAllocationRoleMenuIdAsync(Guid roleId);
    }
}

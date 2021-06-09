using Sukt.Core.Dtos.Identity.UserRole;
using Sukt.Module.Core;
using Sukt.Module.Core.OperationResult;
using System;
using System.Threading.Tasks;

namespace Sukt.Core.Application.Identity.UserRole
{
    public interface IUserRoleContract : IScopedDependency
    {
        /// <summary>
        /// 用户分配角色
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<OperationResponse> AllocationRoleAsync(UserRoleInputDto dto);
        /// <summary>
        /// 获取用户角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<OperationResponse> GetLoadUserRoleAsync(Guid id);
    }
}

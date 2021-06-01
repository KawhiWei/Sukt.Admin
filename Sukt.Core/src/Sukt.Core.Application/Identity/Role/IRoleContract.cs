using Sukt.Core.Dtos.Identity.Role;
using SuktCore.Shared;
using SuktCore.Shared.Entity;
using SuktCore.Shared.Extensions.ResultExtensions;
using SuktCore.Shared.OperationResult;
using System;
using System.Threading.Tasks;

namespace Sukt.Core.Application.Identity.Role
{
    public interface IRoleContract : IScopedDependency
    {
        /// <summary>
        /// 创建角色及分配权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OperationResponse> CreateAsync(RoleInputDto input);

        /// <summary>
        /// 修改角色及分配权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OperationResponse> UpdateAsync(RoleInputDto input);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<OperationResponse> DeleteAsync(Guid id);

        /// <summary>
        /// 分页获取角色
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IPageResult<RoleOutPutPageDto>> GetPageAsync(PageRequest request);
        /// <summary>
        /// 角色分配权限
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<OperationResponse> AllocationRoleMenuAsync(RoleMenuInputDto dto);
    }
}

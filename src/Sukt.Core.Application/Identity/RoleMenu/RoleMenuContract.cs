using Microsoft.EntityFrameworkCore;
using Sukt.Core.Domain.Models.Authority;
using Sukt.Module.Core.Entity;
using Sukt.Module.Core.Enums;
using Sukt.Module.Core.OperationResult;
using Sukt.Module.Core.ResultMessageConst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.Core.Application.Identity.RoleMenu
{
    public class RoleMenuContract : IRoleMenuContract
    {
        private readonly IEFCoreRepository<RoleMenuEntity, Guid> _roleMenuRepository;

        public RoleMenuContract(IEFCoreRepository<RoleMenuEntity, Guid> roleMenuRepository)
        {
            _roleMenuRepository = roleMenuRepository;
        }
        /// <summary>
        /// 角色分配菜单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<OperationResponse> AllocationRoleAsync(Guid roleId, Guid[] roleids)
        {
            return await _roleMenuRepository.UnitOfWork.UseTranAsync(async () =>
            {
                await _roleMenuRepository.DeleteBatchAsync(x => x.RoleId == roleId);
                if (roleids?.Any() == true)
                {
                    await _roleMenuRepository.InsertAsync(roleids.Select(x => new RoleMenuEntity(roleId, x)).ToArray());
                }
                return new OperationResponse(ResultMessage.AllocationSucces, OperationEnumType.Success);
            });
        }
        /// <summary>
        /// 获取角色菜单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<OperationResponse> GetAllocationRoleMenuIdAsync(Guid roleId)
        {
            return new OperationResponse(ResultMessage.DataSuccess, await _roleMenuRepository.NoTrackEntities.Where(x => x.RoleId == roleId).Select(x => x.MenuId).ToListAsync(), OperationEnumType.Success);
        }
    }
}

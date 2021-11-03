using Microsoft.EntityFrameworkCore;
using Sukt.Core.Domain.Models;
using Sukt.Core.Dtos.Identity.UserRole;
using Sukt.Module.Core.Entity;
using Sukt.Module.Core.Enums;
using Sukt.Module.Core.Extensions;
using Sukt.Module.Core.OperationResult;
using Sukt.Module.Core.ResultMessageConst;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sukt.Core.Application.Identity.UserRole
{
    public class UserRoleContract : IUserRoleContract
    {
        private readonly IEFCoreRepository<UserRoleEntity, Guid> _userRoleRepository = null;
        public UserRoleContract(IEFCoreRepository<UserRoleEntity, Guid> userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }
        /// <summary>
        /// 用户分配角色
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<OperationResponse> AllocationRoleAsync(Guid userId, Guid[] roleids)
        {
            return await _userRoleRepository.UnitOfWork.UseTranAsync(async () =>
            {
                await _userRoleRepository.DeleteBatchAsync(x => x.UserId == userId);
                if (roleids?.Any() == true)
                {
                    await _userRoleRepository.InsertAsync(roleids.Select(x => new UserRoleEntity(userId, x)).ToArray());
                }
                return new OperationResponse(ResultMessage.AllocationSucces, OperationEnumType.Success);
            });
        }
        /// <summary>
        /// 获取用户角色
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<OperationResponse> GetLoadUserRoleAsync(Guid id)
        {
            return new OperationResponse(ResultMessage.LoadSucces, await _userRoleRepository.NoTrackEntities.Where(x => x.UserId == id).Select(x => x.RoleId).ToListAsync(), OperationEnumType.Success);
        }
    }
}

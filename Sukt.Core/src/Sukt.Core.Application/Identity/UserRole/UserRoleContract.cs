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
        public async Task<OperationResponse> AllocationRoleAsync(UserRoleInputDto dto)
        {
            //dto.NotNull(nameof(dto));
            //return await _userRoleRepository.UnitOfWork.UseTranAsync(async () =>
            //{
            //    await _userRoleRepository.DeleteBatchAsync(x => x.UserId == dto.Id);
            //    if (dto.RoleIds?.Any() == true)
            //    {
            //        await _userRoleRepository.InsertAsync(dto.RoleIds.Select(x => new UserRoleEntity
            //        {
            //            RoleId = x,
            //            UserId = dto.Id
            //        }).ToArray());
            //    }
            await Task.CompletedTask;
                return new OperationResponse(ResultMessage.AllocationSucces, OperationEnumType.Success);
            //});
        }
        /// <summary>
        /// 获取用户角色
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<OperationResponse> GetLoadUserRoleAsync(Guid id)
        {
            await Task.CompletedTask;
            return new OperationResponse(ResultMessage.LoadSucces, /*await _userRoleRepository.NoTrackEntities.Where(x => x.UserId == id).Select(x => x.RoleId).ToListAsync(),*/ OperationEnumType.Success);
        }
    }
}

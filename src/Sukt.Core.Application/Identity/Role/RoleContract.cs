using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sukt.Core.Domain.Models;
using Sukt.Core.Domain.Models.Authority;
using Sukt.Core.Dtos.Identity.Role;
using Sukt.Module.Core.Entity;
using Sukt.Module.Core.Extensions;
using Sukt.Module.Core.Extensions.ResultExtensions;
using Sukt.Module.Core.OperationResult;
using System;
using System.Linq;
using System.Threading.Tasks;
using Sukt.Module.Core.ResultMessageConst;
using Sukt.Module.Core.Enums;

namespace Sukt.Core.Application.Identity.Role
{
    public class RoleContract : IRoleContract
    {
        private readonly IEFCoreRepository<RoleMenuEntity, Guid> _roleMenuRepository;
        private readonly RoleManager<RoleEntity> _roleManager;

        public RoleContract(IEFCoreRepository<RoleMenuEntity, Guid> roleMenuRepository, RoleManager<RoleEntity> roleManager)
        {
            _roleMenuRepository = roleMenuRepository;
            _roleManager = roleManager;
        }

        /// <summary>
        /// 创建角色及分配权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<OperationResponse> CreateAsync(RoleInputDto input)
        {
            input.NotNull(nameof(input));
            var entity = new RoleEntity(input.Name, input.NormalizedName, input.IsAdmin);
            return (await _roleManager.CreateAsync(entity)).ToOperationResponse();
        }

        public async Task<OperationResponse> AllocationRoleMenuAsync(RoleMenuInputDto dto)
        {
            dto.NotNull(nameof(dto));
            return await _roleMenuRepository.UnitOfWork.UseTranAsync(async () =>
            {
                await Task.CompletedTask;
                //await _roleMenuRepository.DeleteBatchAsync(x => x.RoleId == dto.Id);
                //await _roleMenuRepository.InsertAsync(dto.MenuIds.Select(x => new RoleMenuEntity
                //{
                //    RoleId = dto.Id,
                //    MenuId = x,
                //}).ToArray());
                return new OperationResponse(ResultMessage.AllocationSucces, OperationEnumType.Success);
            });
        }

        /// <summary>
        /// 修改角色及分配权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<OperationResponse> UpdateAsync(Guid id, RoleInputDto input)
        {
            input.NotNull(nameof(input));
            var entity = await _roleManager.FindByIdAsync(id.ToString());
            entity.SetFunc(input.Name, input.NormalizedName, input.IsAdmin);
            return (await _roleManager.UpdateAsync(entity)).ToOperationResponse();
        }

        public async Task<OperationResponse> DeleteAsync(Guid id)
        {
            id.NotNull(nameof(id));
            var role = await _roleManager.FindByIdAsync(id.ToString());
            await _roleManager.DeleteAsync(role);
            return new OperationResponse(ResultMessage.DeleteSuccess, OperationEnumType.Success);
        }

        /// <summary>
        /// 分页获取角色
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IPageResult<RoleOutPutPageDto>> GetPageAsync(PageRequest request)
        {
            request.NotNull(nameof(request));
            return await _roleManager.Roles.AsNoTracking().ToPageAsync<RoleEntity, RoleOutPutPageDto>(request);
        }
        /// <summary>
        /// 加载角色
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<OperationResponse> LoadFormAsync(Guid id)
        {
            var entity = await _roleManager.FindByIdAsync(id.ToString());
            return new OperationResponse(ResultMessage.AllocationSucces, entity.MapTo<RoleOutPutPageDto>(), OperationEnumType.Success);
        }
        
    }
}

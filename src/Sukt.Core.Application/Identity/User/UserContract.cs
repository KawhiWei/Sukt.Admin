using IDN.Services.BasicsService.Dtos.Identity.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sukt.Core.Domain.Models;
using Sukt.Core.Dtos;
using Sukt.Module.Core.AjaxResult;
using Sukt.Module.Core.Entity;
using Sukt.Module.Core.Enums;
using Sukt.Module.Core.Extensions;
using Sukt.Module.Core.Extensions.OrderExtensions;
using Sukt.Module.Core.Extensions.ResultExtensions;
using Sukt.Module.Core.OperationResult;
using Sukt.Module.Core.ResultMessageConst;
using System;
using System.Threading.Tasks;

namespace Sukt.Core.Application
{
    public class UserContract : IUserContract
    {
        private readonly UserManager<UserEntity> _userManager = null;
        private readonly IUnitOfWork _unitOfWork = null;

        public UserContract(UserManager<UserEntity> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResponse> InsertAsync(UserInputDto input)
        {
            input.NotNull(nameof(input));
            var user = new UserEntity(input.Birthday, input.Education, input.TechnicalLevel, input.IdCard, input.IsEnable, input.Duties,
                input.Department, input.UserType,input.UserName,input.NormalizedUserName,input.NickName,input.Email,input.Email,false,input.PasswordHash,
                "",null,Guid.NewGuid().ToString(),input.PhoneNumber,false,false,null,false,0,false,input.Sex);
            var passwordHash = input.PasswordHash;
            var result = passwordHash.IsNullOrEmpty() ? await _userManager.CreateAsync(user) : await _userManager.CreateAsync(user, passwordHash);
            return result.ToOperationResponse();
            // return await _unitOfWork.UseTranAsync(async () =>
            //{

            //    if (!result.Succeeded)
            //        return result.ToOperationResponse();
            //    if (input.RoleIds?.Any() == true)
            //    {
            //        await _userRoleRepository.InsertAsync(input.RoleIds.Select(x => new UserRoleEntity
            //        {
            //            RoleId = x,
            //            UserId = user.Id,
            //        }).ToArray());
            //    }
            //    return new OperationResponse(ResultMessage.InsertSuccess, OperationEnumType.Success);
            //});
        }

        public async Task<OperationResponse> LoadFormAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var userdto = user.MapTo<UserLoadFormOutputDto>();
            //userdto.RoleIds = await _userRoleRepository.NoTrackEntities.Where(x => x.UserId == user.Id).Select(x => x.RoleId).ToListAsync();
            return new OperationResponse(ResultMessage.LoadSucces, userdto, OperationEnumType.Success);
        }

        public async Task<OperationResponse> UpdateAsync(Guid id, UserInputDto input)
        {
            input.NotNull(nameof(input));
            var user = await _userManager.FindByIdAsync(id.ToString());
            user.SetFunc(input.Birthday, input.Education, input.TechnicalLevel, input.IdCard, input.IsEnable, input.Duties,
                input.Department, input.UserType);
            user.SetBaseFunc(input.UserName, input.NormalizedUserName, input.NickName, input.Email, input.HeadImg, input.PhoneNumber, input.Sex);
            return (await _userManager.UpdateAsync(user)).ToOperationResponse();
            //return result;
            //return await _unitOfWork.UseTranAsync(async () =>
            //{

            //    if (!result.Succeeded)
            //        return result.ToOperationResponse();
            //    await _userRoleRepository.DeleteBatchAsync(x => x.UserId == input.Id);
            //    if (input.RoleIds?.Any() == true)
            //    {
            //        await _userRoleRepository.InsertAsync(input.RoleIds.Select(x => new UserRoleEntity
            //        {
            //            RoleId = x,
            //            UserId = user.Id,
            //        }).ToArray());
            //    }
            //    return new OperationResponse(ResultMessage.UpdateSuccess, OperationEnumType.Success);
            //});
        }

        public async Task<OperationResponse> DeleteAsync(Guid id)
        {
            id.NotNull(nameof(id));
            var user = await _userManager.FindByIdAsync(id.ToString());
            return await _unitOfWork.UseTranAsync(async () =>
            {
                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                    return result.ToOperationResponse();
                return new OperationResponse(ResultMessage.DeleteSuccess, OperationEnumType.Success);
            });
        }
        public async Task<IPageResult<UserPageOutputDto>> GetPageAsync(PageRequest request)
        {
            request.NotNull(nameof(request));
            OrderCondition<UserEntity>[] orderConditions = new OrderCondition<UserEntity>[] { new OrderCondition<UserEntity>(o => o.CreatedAt, SortDirectionEnum.Descending) };
            request.OrderConditions = orderConditions;
            return await _userManager.Users.AsNoTracking().ToPageAsync<UserEntity, UserPageOutputDto>(request);
        }
        
    }
}

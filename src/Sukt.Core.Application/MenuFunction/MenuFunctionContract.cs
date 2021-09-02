using Microsoft.EntityFrameworkCore;
using Sukt.Core.Domain.Models;
using Sukt.Core.Domain.Models.Menu;
using Sukt.Core.Dtos.MenuFunction;
using Sukt.Module.Core.Entity;
using Sukt.Module.Core.Extensions;
using Sukt.Module.Core.OperationResult;
using System;
using System.Linq;
using System.Threading.Tasks;
using Sukt.Module.Core.Enums;
using Sukt.Module.Core.ResultMessageConst;
namespace Sukt.Core.Application.MenuFunction
{
    public class MenuFunctionContract : IMenuFunctionContract
    {
        private readonly IEFCoreRepository<MenuFunctionEntity, Guid> _menuFunctionRepository;
        private readonly IEFCoreRepository<FunctionEntity, Guid> _functionRepository;

        public MenuFunctionContract(IEFCoreRepository<MenuFunctionEntity, Guid> menuFunctionRepository, IEFCoreRepository<FunctionEntity, Guid> functionRepository)
        {
            _menuFunctionRepository = menuFunctionRepository;
            _functionRepository = functionRepository;
        }
        /// <summary>
        /// 分配菜单功能
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<OperationResponse> AllocationMenuFunctionAsync(MenuFunctionInputDto input)
        {
            input.NotNull(nameof(input));
            //return await _menuFunctionRepository.UnitOfWork.UseTranAsync(async () =>
            //{
            //    await _menuFunctionRepository.DeleteBatchAsync(x => x.MenuId == input.Id);
            //    await _menuFunctionRepository.InsertAsync(input.FuncIds.Select(x => new MenuFunctionEntity
            //    {
            //        MenuId = input.Id,
            //        FunctionId = x
            //    }).ToArray());
            await Task.CompletedTask;
                return new OperationResponse(ResultMessage.AllocationSucces, OperationEnumType.Success);
            //});
        }
        /// <summary>
        /// 获取菜单功能
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<OperationResponse> GetLoadMenuFunctionAsync(Guid id)
        {
            await Task.CompletedTask;
            //var menuIds = await _menuFunctionRepository.NoTrackEntities.Where(x => x.MenuId == id).Select(x => x.MenuId).ToListAsync();
            //return new OperationResponse(ResultMessage.LoadSucces, await _functionRepository.NoTrackEntities.Where(x => menuIds.Contains(x.Id)).Select(x => new MenuFunctionOutListDto
            //{
            //    LinkUrl = x.LinkUrl,
            //    Description = x.Description,
            //    IsEnabled = x.IsEnabled,
            //    Name = x.Name,
            //}).ToListAsync(), OperationEnumType.Success);
            return new OperationResponse(ResultMessage.LoadSucces, OperationEnumType.Success);

        }
    }
}

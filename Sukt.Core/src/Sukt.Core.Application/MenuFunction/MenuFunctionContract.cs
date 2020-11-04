using Microsoft.EntityFrameworkCore;
using Sukt.Core.Domain.Models;
using Sukt.Core.Domain.Models.Function;
using Sukt.Core.Dtos.MenuFunction;
using Sukt.Core.Shared.Entity;
using Sukt.Core.Shared.Enums;
using Sukt.Core.Shared.Extensions;
using Sukt.Core.Shared.OperationResult;
using Sukt.Core.Shared.ResultMessageConst;
using System;
using System.Linq;
using System.Threading.Tasks;

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
            return await _menuFunctionRepository.UnitOfWork.UseTranAsync(async () =>
            {
                await _menuFunctionRepository.DeleteBatchAsync(x => x.MenuId == input.Id);
                await _menuFunctionRepository.InsertAsync(input.FuncIds.Select(x => new MenuFunctionEntity
                {
                    MenuId = input.Id,
                    FunctionId = x
                }).ToArray());
                return new OperationResponse(ResultMessage.AllocationSucces, OperationEnumType.Success);
            });
        }
        /// <summary>
        /// 获取菜单功能
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<OperationResponse> GetLoadMenuFunctionAsync(Guid id)
        {

            var menuIds = await _menuFunctionRepository.NoTrackEntities.Where(x => x.MenuId == id).Select(x => x.MenuId).ToListAsync();
            return new OperationResponse(ResultMessage.LoadSucces, await _functionRepository.NoTrackEntities.Where(x => menuIds.Contains(x.Id)).Select(x => new MenuFunctionOutListDto
            {
                LinkUrl = x.LinkUrl,
                Description = x.Description,
                IsEnabled = x.IsEnabled,
                Name = x.Name,
            }).ToListAsync(), OperationEnumType.Success);

        }
    }
}

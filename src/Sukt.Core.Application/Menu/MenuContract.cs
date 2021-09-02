using Microsoft.EntityFrameworkCore;
using Sukt.Core.Domain.Models;
using Sukt.Core.Domain.Models.Menu;
using Sukt.Core.Dtos.Menu;
using Sukt.Module.Core.Entity;
using Sukt.Module.Core.Enums;
using Sukt.Module.Core.Extensions;
using Sukt.Module.Core.OperationResult;
using Sukt.Module.Core.ResultMessageConst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sukt.Core.Application
{
    public class MenuContract : IMenuContract
    {
        private readonly IEFCoreRepository<MenuEntity, Guid> _menu;
        private readonly IEFCoreRepository<MenuFunctionEntity, Guid> _menuFunction;

        public MenuContract(IEFCoreRepository<MenuEntity, Guid> menu, IEFCoreRepository<MenuFunctionEntity, Guid> menuFunction)
        {
            _menu = menu;
            _menuFunction = menuFunction;
        }

        public async Task<OperationResponse> GetMenuTableAsync()
        {

            //Console.WriteLine($"--------服务层当前线程ID{ Thread.CurrentThread.ManagedThreadId}");

            var list = await _menu.NoTrackEntities.ToTreeResultAsync<MenuEntity, MenuTableOutputDto>(
                 (p, c) =>
                 {
                     return c.ParentId == Guid.Empty;
                 },
                 (p, c) =>
                 {
                     return p.Id == c.ParentId;
                 },
                 (p, datalist) =>
                 {
                     if (p.Children == null)
                     {
                         p.Children = new List<MenuTableOutputDto>();
                     }
                     p.Children.AddRange(datalist);
                 }
                 );
            OperationResponse operationResponse = new OperationResponse(ResultMessage.DataSuccess, list, OperationEnumType.Success);
            return operationResponse;
        }

        public async Task<OperationResponse> InsertAsync(MenuInputDto input)
        {
            input.NotNull(nameof(input));

            return await _menu.InsertAsync(input);
            //return await _menu.UnitOfWork.UseTranAsync(async () =>
            //{

            //    if (input.FuncIds?.Any() == true)
            //    {
            //        int count = await _menuFunction.InsertAsync(input.FuncIds.Select(x => new MenuFunctionEntity
            //        {
            //            MenuId = input.Id,
            //            FunctionId = x
            //        }).ToArray());
            //    }
            //    return new OperationResponse(ResultMessage.InsertSuccess, OperationEnumType.Success);
            //});
        }

        public async Task<OperationResponse> UpdateAsync(MenuInputDto input)
        {
            input.NotNull(nameof(input));
            return await _menu.UpdateAsync(input);

            //return await _menu.UnitOfWork.UseTranAsync(async () =>
            //{
            //    var result = await _menu.UpdateAsync(input);
            //    await _menuFunction.DeleteBatchAsync(x => x.MenuId == input.Id);
            //    if (input.FuncIds?.Any() == true)
            //    {
            //        int count = await _menuFunction.InsertAsync(input.FuncIds.Select(x => new MenuFunctionEntity
            //        {
            //            MenuId = input.Id,
            //            FunctionId = x
            //        }).ToArray());
            //    }
            //    return new OperationResponse(ResultMessage.UpdateSuccess, OperationEnumType.Success);
            //});
        }

        public async Task<OperationResponse> DeleteAsync(Guid id)
        {
            id.NotNull(nameof(id));
            return await _menu.DeleteAsync(id);
        }

        public async Task<OperationResponse> GetLoadFromMenuAsync(Guid id)
        {
            id.NotNull(nameof(id));
            var menu = await _menu.GetByIdAsync(id);
            var menudto = menu.MapTo<MenuLoadOutputDto>();
            //menudto.FuncIds = await _menuFunction.NoTrackEntities.Where(x => x.MenuId == id).Select(x => x.FunctionId).ToListAsync();
            return new OperationResponse(ResultMessage.DataSuccess, OperationEnumType.Success);
        }

        public async Task<OperationResponse> GetUserMenuTreeAsync()
        {
            var list = await _menu.NoTrackEntities.ToTreeResultAsync<MenuEntity, RouterMenuOutput>(
                (p, c) =>
                {
                    return c.ParentId == Guid.Empty;
                },
                (p, c) =>
                {
                    return p.Id == c.ParentId;
                },
                (p, datalist) =>
                {
                    p.Buttons.AddRange(datalist.Where(a => a.Type == MenuEnum.Button));
                    p.Children.AddRange(datalist.Where(a => a.Type == MenuEnum.MenuType));
                    p.Tabs.AddRange(datalist.Where(a => a.Type == MenuEnum.Tab));
                }
                );
            OperationResponse operationResponse = new OperationResponse(ResultMessage.DataSuccess, list, OperationEnumType.Success);
            return operationResponse;
        }
    }
}

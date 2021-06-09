using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sukt.Core.Domain.Models.Function;
using Sukt.Core.Dtos.Function;
using Sukt.Module.Core.Entity;
using Sukt.Module.Core.Enums;
using Sukt.Module.Core.Exceptions;
using Sukt.Module.Core.Extensions;
using Sukt.Module.Core.Extensions.OrderExtensions;
using Sukt.Module.Core.Extensions.ResultExtensions;
using Sukt.Module.Core.OperationResult;
using Sukt.Module.Core.ResultMessageConst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sukt.Core.Application
{
    public class FunctionContract : IFunctionContract
    {
        private readonly IEFCoreRepository<FunctionEntity, Guid> _functionRepository;

        public FunctionContract(IEFCoreRepository<FunctionEntity, Guid> functionRepository)
        {
            _functionRepository = functionRepository;
        }

        public async Task<OperationResponse> DeleteAsync(Guid id)
        {
            id.NotEmpty(nameof(id));
            return await _functionRepository.DeleteAsync(id);
        }

        private IQueryable<FunctionEntity> Entities => _functionRepository.NoTrackEntities;

        public async Task<OperationResponse> InsertAsync(FunctionInputDto input)
        {
            input.NotNull(nameof(input));
            return await _functionRepository.InsertAsync(input, async f =>
             {
                 bool isExist = await this.Entities.Where(x => x.LinkUrl.ToLower() == input.LinkUrl.ToLower()).AnyAsync();
                 if (isExist)
                     throw new SuktAppException("此功能已存在!!!");
             });
        }

        public async Task<IPageResult<FunctionOutputPageDto>> GetFunctionPageAsync(PageRequest request)
        {
            request.NotNull(nameof(request));
            OrderCondition<FunctionEntity>[] orderConditions = new OrderCondition<FunctionEntity>[] { new OrderCondition<FunctionEntity>(o => o.CreatedAt, SortDirectionEnum.Descending) };
            request.OrderConditions = orderConditions;
            return await _functionRepository.NoTrackEntities.ToPageAsync<FunctionEntity, FunctionOutputPageDto>(request);
        }

        public async Task<OperationResponse> UpdateAsync(FunctionInputDto input)
        {
            input.NotNull(nameof(input));
            return await _functionRepository.UpdateAsync(input, async (f, e) =>
            {
                bool isExist = await this.Entities.Where(o => o.Id != f.Id && o.LinkUrl.ToLower() == f.LinkUrl.ToLower()).AnyAsync();
                if (isExist)
                {
                    throw new SuktAppException("此功能已存在!!!");
                }
            });
        }

        /// <summary>
        /// 获取功能下拉框列表
        /// </summary>
        /// <returns></returns>
        public async Task<OperationResponse<IEnumerable<SelectListItem>>> GetFunctionSelectListItemAsync()
        {
            var functions = await _functionRepository.NoTrackEntities.OrderBy(o => o.Name).Select(x => new SelectListItem
            {
                Value = x.Id.ToString().ToLower(),
                Text = x.Name,
                Selected = false
            }).ToListAsync();
            return new OperationResponse<IEnumerable<SelectListItem>>(ResultMessage.DataSuccess, functions, OperationEnumType.Success);
        }
    }
}
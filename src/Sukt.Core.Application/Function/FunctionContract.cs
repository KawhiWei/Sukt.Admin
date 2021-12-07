using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sukt.Core.Domain.Models.Menu;
using Sukt.Core.Dtos.Function;
using Sukt.Module.Core.Entity;
using Sukt.Module.Core.Enums;
using Sukt.Module.Core.Exceptions;
using Sukt.Module.Core.Extensions;
using Sukt.Module.Core.Extensions.OrderExtensions;
using Sukt.Module.Core.Extensions.ResultExtensions;
using Sukt.Module.Core.OperationResult;
using Sukt.Module.Core.ResultMessageConst;
using Sukt.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Sukt.Core.Application
{
    public class FunctionContract : IFunctionContract
    {
        private readonly IEFCoreRepository<FunctionEntity, Guid> _functionRepository;
        private readonly IRedisRepository _redisRepository;
        public FunctionContract(IEFCoreRepository<FunctionEntity, Guid> functionRepository, IRedisRepository redisRepository)
        {
            _functionRepository = functionRepository;
            _redisRepository = redisRepository;
        }
        private IQueryable<FunctionEntity> Entities => _functionRepository.NoTrackEntities;

        public async Task<OperationResponse> InsertAsync(FunctionInputDto input)
        {
            input.NotNull(nameof(input));
            FunctionEntity entity = new(input.Name, input.Description, input.IsEnabled, input.LinkUrl);
            return await _functionRepository.InsertAsync(entity, async f =>
             {
                 bool isExist = await this.Entities.Where(x => x.LinkUrl.ToLower() == input.LinkUrl.ToLower()).AnyAsync();
                 if (isExist)
                     throw new SuktAppException("此功能已存在!!!");
             });
        }

        public async Task<IPageResult<FunctionOutputPageDto>> GetPageAsync(PageRequest request)
        {
            request.NotNull(nameof(request));
            OrderCondition<FunctionEntity>[] orderConditions = new OrderCondition<FunctionEntity>[] { new OrderCondition<FunctionEntity>(o => o.CreatedAt, SortDirectionEnum.Descending) };
            request.OrderConditions = orderConditions;
            return await _functionRepository.NoTrackEntities.ToPageAsync<FunctionEntity, FunctionOutputPageDto>(request);
        }
        /// <summary>
        /// 加载表单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<OperationResponse> LoadFromAsync(Guid id)
        {
            FunctionEntity entity = await _functionRepository.GetByIdAsync(id);
            Console.WriteLine($"第二步被调用方法线程Id：{Thread.CurrentThread.ManagedThreadId}");
            return new OperationResponse(ResultMessage.DataSuccess, entity.MapTo<FunctionOutputPageDto>(), OperationEnumType.Success);
        }

        public async Task<OperationResponse> UpdateAsync(Guid id, FunctionInputDto input)
        {
            input.NotNull(nameof(input));
            FunctionEntity entity = await _functionRepository.GetByIdAsync(id);
            entity.SetFiled(input.Name, input.Description, input.IsEnabled, input.LinkUrl);
            bool isExist = await this.Entities.Where(o => o.Id != id && o.LinkUrl.ToLower() == entity.LinkUrl.ToLower()).AnyAsync();
            if (isExist)
            {
                return new OperationResponse("此功能已存在!!!", OperationEnumType.Error);
            }
            return await _functionRepository.UpdateAsync(entity);
        }
        public async Task<OperationResponse> DeleteAsync(Guid id)
        {
            id.NotEmpty(nameof(id));
            return await _functionRepository.DeleteAsync(id);
        }

        public void TestA()
        {
            Console.WriteLine($"第一步被调用方法线程Id：{Thread.CurrentThread.ManagedThreadId}");
        }
        public void TestB()
        {
            Console.WriteLine($"第三步被调用方法线程Id：{Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
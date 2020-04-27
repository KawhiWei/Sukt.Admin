using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.Aop.AttributeAOP;
using Sukt.Core.Application.Contracts;
using Sukt.Core.Domain.DomainRepository.DictionaryRepository;
using Sukt.Core.Domain.Models.DataDictionary;
using Sukt.Core.Dtos.DataDictionaryDto;
using Sukt.Core.Shared.Attributes.Dependency;
using Sukt.Core.Shared.Entity;
using Sukt.Core.Shared.Extensions;
using Sukt.Core.Shared.Extensions.OrderExtensions;
using Sukt.Core.Shared.Extensions.PageExyensions;
using Sukt.Core.Shared.Extensions.ResultExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.Core.Application
{
    /// <summary>
    /// 数据字典应用实现层
    /// </summary>
    [Dependency(ServiceLifetime.Scoped)]
    public class DictionaryContract : IDictionaryContract
    {
        private readonly IDataDictionaryRepository _dataDictionary;
        public DictionaryContract(IDataDictionaryRepository dataDictionary)
        {
            _dataDictionary = dataDictionary;
        }
        //[NonGlobalAopTran]
        public async Task<bool> InsertAsync(DataDictionaryInputDto input)
        {
            input.NotNull(nameof(input));
            var entity = input.MapTo<DataDictionaryEntity>();
            return await _dataDictionary.InsertAsync(entity) > 0;
        }
        public async Task<PageResult<DataDictionaryOutDto>> GetResultAsync(BaseQuery query)
        {
            var param = new PageParameters(query.PageIndex, query.PageRow);
            param.OrderConditions = new OrderCondition[]
            {
                new OrderCondition(query.SortName,query.SortDirection)
            };
            return await _dataDictionary.NoTrackEntities.ToPageAsync<DataDictionaryEntity, DataDictionaryOutDto>(x => x.IsDeleted == false, param);
        }
        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<TreeData<TreeDictionaryOutDto>> GetTreeAsync()
        {
            var list = await _dataDictionary.NoTrackEntities.ToTreeResultAsync<DataDictionaryEntity, TreeDictionaryOutDto>(
                (p, c) =>
                {
                    return c.ParentId == null || c.ParentId == Guid.Empty;
                },
                (p, c) =>
                {
                    return p.Id == c.ParentId;
                },
                (p, datalist) =>
                {
                    if (p.Children == null)
                    {
                        p.Children = new List<TreeDictionaryOutDto>();
                    }
                    p.Children.AddRange(datalist);
                }
                );
            return list;
        }
    }
}

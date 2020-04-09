using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.Application.Contracts.DictionaryContract;
using Sukt.Core.Domain.DomainRepository.DictionaryRepository;
using Sukt.Core.Domain.Models.DataDictionary;
using Sukt.Core.Dtos.DataDictionaryDto;
using Sukt.Core.Shared.Attributes.Dependency;
using Sukt.Core.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.Core.Application.DictionaryContract
{
    /// <summary>
    /// 数据字典应用实现层
    /// </summary>
    [Dependency(ServiceLifetime.Scoped)]
    public class DictionaryContract: IDictionaryContract
    {
        private readonly IDataDictionaryRepository _dataDictionary;

        public DictionaryContract(IDataDictionaryRepository dataDictionary)
        {
            _dataDictionary = dataDictionary;
        }
        public async Task<bool> InsertAsync(DataDictionaryInputDto input)
        {
            input.NotNull(nameof(input));
            var entity = input.MapTo<DataDictionaryEntity>();
            return  await _dataDictionary.InsertAsync(entity)>0;
        }
    }
}

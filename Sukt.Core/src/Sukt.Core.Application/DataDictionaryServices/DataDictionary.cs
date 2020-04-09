using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.Application.Contracts.IDataDictionaryServices;
using Sukt.Core.Domain.DataDictionary;
using Sukt.Core.Shared.Attributes.Dependency;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.Core.Application.DataDictionaryServices
{
    [Dependency(ServiceLifetime.Scoped)]
    public class DataDictionary : IDataDictionary
    {
        private readonly IDataDictionaryDomain _dataDictionary=null;

        public DataDictionary(IDataDictionaryDomain dataDictionary)
        {
            _dataDictionary = dataDictionary;
        }

        public async Task GetDataDictionaryAsync()
        {
            await _dataDictionary.InsertAsync(new Domain.Models.DataDictionary.DataDictionaryEntity
            {
                Code = "15212",
                Title = "15212",
                Value = "112",
                ParentId = Guid.Empty,
                Sort=1,
                CreatedAt=DateTime.Now,
                LastModifedAt=DateTime.Now,
            }) ;
        }
    }
}

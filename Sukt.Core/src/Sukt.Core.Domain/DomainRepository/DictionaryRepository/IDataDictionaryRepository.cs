using System;
using System.Collections.Generic;
using System.Text;
using Sukt.Core.Domain.ISuktBaseRepository;
using Sukt.Core.Domain.Models.DataDictionary;

namespace Sukt.Core.Domain.DomainRepository.DictionaryRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataDictionaryRepository: IEFCoreRepository<DataDictionaryEntity,Guid>
    {

    }
}

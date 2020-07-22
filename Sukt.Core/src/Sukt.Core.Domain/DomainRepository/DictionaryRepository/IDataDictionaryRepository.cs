using System;
using System.Collections.Generic;
using System.Text;
using Sukt.Core.Domain.Model.DataDictionary;
using Sukt.Core.EntityFrameworkCore;

namespace Sukt.Core.Domain.DomainRepository.DictionaryRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataDictionaryRepository: IEFCoreRepository<DataDictionaryEntity,Guid>
    {

    }
}

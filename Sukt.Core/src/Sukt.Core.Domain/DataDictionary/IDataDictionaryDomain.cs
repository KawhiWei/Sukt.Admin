using Sukt.Core.Domain.Models.DataDictionary;
using Sukt.Core.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Domain.DataDictionary
{
    public interface IDataDictionaryDomain: IEFCoreRepository<DataDictionaryEntity,Guid>
    {

    }
}

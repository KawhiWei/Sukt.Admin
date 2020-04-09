using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.Core.Application.Contracts.IDataDictionaryServices
{
    public interface IDataDictionary
    {
        Task GetDataDictionaryAsync();
    }
}

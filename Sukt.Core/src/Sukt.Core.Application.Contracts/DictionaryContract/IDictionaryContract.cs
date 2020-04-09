using Sukt.Core.Dtos.DataDictionaryDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.Core.Application.Contracts.DictionaryContract
{
    public interface IDictionaryContract
    {
        Task<bool> InsertAsync(DataDictionaryInputDto input);
    }
}

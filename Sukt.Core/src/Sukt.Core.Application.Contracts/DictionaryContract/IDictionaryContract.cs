using Sukt.Core.Dtos.DataDictionaryDto;
using Sukt.Core.Shared.Entity;
using Sukt.Core.Shared.Extensions.ResultExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.Core.Application.Contracts.DictionaryContract
{
    public interface IDictionaryContract
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> InsertAsync(DataDictionaryInputDto input);
        /// <summary>
        /// 分页获取
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<PageResult<DataDictionaryOutDto>> GetResultAsync(BaseQuery query);
        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<TreeData<TreeDictionaryOutDto>> GetTreeAsync();
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;
using Sukt.Core.Dtos.Function;
using Sukt.Module.Core.Entity;
using Sukt.Module.Core.OperationResult;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sukt.Module.Core;
using Sukt.Module.Core.Extensions.ResultExtensions;

namespace Sukt.Core.Application
{
    /// <summary>
    /// 功能管理
    /// </summary>
    public interface IFunctionContract : IScopedDependency
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OperationResponse> InsertAsync(FunctionInputDto input);

        ///// <summary>
        ///// 分页获取
        ///// </summary>
        ///// <param name="query"></param>
        ///// <returns></returns>
        //Task<PageResult<DataDictionaryOutDto>> GetResultAsync();
        ///// <summary>
        ///// 获取树形数据
        ///// </summary>
        ///// <param name="query"></param>
        ///// <returns></returns>
        //Task<TreeData<TreeDictionaryOutDto>> GetTreeAsync();
        /// <summary>
        /// 修改一行数据
        /// </summary>
        /// <returns></returns>
        Task<OperationResponse> UpdateAsync(FunctionInputDto input);

        /// <summary>
        /// 删除一行数据
        /// </summary>
        /// <returns></returns>
        Task<OperationResponse> DeleteAsync(Guid id);

        /// <summary>
        /// 分页获取功能
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IPageResult<FunctionOutputPageDto>> GetFunctionPageAsync(PageRequest request);

        /// <summary>
        /// 获取功能下拉框列表
        /// </summary>
        /// <returns></returns>
        Task<OperationResponse<IEnumerable<SelectListItem>>> GetFunctionSelectListItemAsync();
    }
}
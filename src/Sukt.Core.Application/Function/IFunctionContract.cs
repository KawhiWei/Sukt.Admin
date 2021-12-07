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
        /// <summary>
        /// 修改一行数据
        /// </summary>
        /// <returns></returns>
        Task<OperationResponse> UpdateAsync(Guid id,FunctionInputDto input);

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
        Task<IPageResult<FunctionOutputPageDto>> GetPageAsync(PageRequest request);
        /// <summary>
        /// 加载表单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<OperationResponse> LoadFromAsync(Guid id);
        void TestA();
        void TestB();

    }
}
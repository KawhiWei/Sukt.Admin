using Sukt.Core.Dtos.MenuFunction;
using Sukt.Core.Shared;
using Sukt.Core.Shared.OperationResult;
using System;
using System.Threading.Tasks;

namespace Sukt.Core.Application.MenuFunction
{
    public interface IMenuFunctionContract : IScopedDependency
    {
        /// <summary>
        /// 分配菜单功能
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OperationResponse> AllocationMenuFunctionAsync(MenuFunctionInputDto input);
        /// <summary>
        /// 获取菜单功能
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OperationResponse> GetLoadMenuFunctionAsync(Guid id);
    }
}

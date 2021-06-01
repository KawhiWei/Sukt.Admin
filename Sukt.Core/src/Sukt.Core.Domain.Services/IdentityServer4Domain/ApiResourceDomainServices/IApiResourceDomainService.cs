using Sukt.Core.Domain.Models.IdentityServerFour;
using SuktCore.Shared;
using SuktCore.Shared.OperationResult;
using System;
using System.Threading.Tasks;

namespace Sukt.Core.Domain.Services.IdentityServer4Domain.ApiResourceDomainServices
{
    public interface IApiResourceDomainService : IScopedDependency
    {
        /// <summary>
        /// 添加Api资源
        /// </summary>
        /// <param name="apiResource"></param>
        /// <returns></returns>
        Task<OperationResponse> CreateApiResourceAsync(ApiResource apiResource);
        /// <summary>
        /// 返回一个对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApiResource> GetLoadAsync(Guid id);
        /// <summary>
        /// 修改一个对象
        /// </summary>
        /// <param name="apiResource"></param>
        /// <returns></returns>
        Task<OperationResponse> UpdateAsync(ApiResource apiResource);
    }
}

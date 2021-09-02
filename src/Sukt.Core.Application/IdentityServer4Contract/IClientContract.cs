using Sukt.Core.Dtos.IdentityServer4Dto;
using Sukt.Module.Core;
using Sukt.Module.Core.OperationResult;
using System;
using System.Threading.Tasks;

namespace Sukt.Core.Application.IdentityServer4Contract
{
    public interface IClientContract : IScopedDependency
    {
        /// <summary>
        /// 添加客户端
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OperationResponse> CreateAsync(ClientInputDto input);
        /// <summary>
        /// 获取一个客户端
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OperationResponse> GetLoadAsync(Guid id);
        /// <summary>
        /// 添加客户端密钥
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OperationResponse> CreateSecretAsync(SecretInputDto input);
        /// <summary>
        /// 添加客户端允许访问范围
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OperationResponse> CreateClientScopeAsync(CommonInputDto input);
        /// <summary>
        /// 添加客户端退出登录Uri
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OperationResponse> CreatePostLogoutRedirectUriAsync(CommonInputDto input);
        /// <summary>
        /// 添加登录回调Uri
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OperationResponse> CreateRedirectUriAsync(CommonInputDto input);
        /// <summary>
        /// 添加允许跨域
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OperationResponse> CreateCorsOriginAsync(CommonInputDto input);
    }
}

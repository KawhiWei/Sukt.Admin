using Sukt.Core.Dtos.IdentityServer4Dto;
using Sukt.Core.Dtos.IdentityServer4Dto.ApiResource;
using Sukt.Module.Core;
using Sukt.Module.Core.OperationResult;
using System.Threading.Tasks;

namespace Sukt.Core.Application.IdentityServer4Contract
{
    public interface IApiResourceContract : IScopedDependency
    {
        /// <summary>
        /// 添加API资源
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OperationResponse> CreateAsync(ApiResourceInputDto input);
        /// <summary>
        /// 添加Api资源密钥
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OperationResponse> CreateApiResourceSecretAsync(SecretInputDto input);
        /// <summary>
        /// 添加Api资源范围
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<OperationResponse> CreateApiResourceScopesAsync(CommonInputDto input);
        /// <summary>
        /// 获取JwtClaimType类型集合
        /// </summary>
        /// <returns></returns>
        OperationResponse GetJwtClaimTypeSelectItem();
    }
}

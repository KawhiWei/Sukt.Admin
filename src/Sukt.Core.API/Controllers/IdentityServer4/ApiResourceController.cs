using Microsoft.AspNetCore.Mvc;
using Sukt.Core.Application.IdentityServer4Contract;
using Sukt.Core.Dtos.IdentityServer4Dto;
using Sukt.Core.Dtos.IdentityServer4Dto.ApiResource;
using Sukt.Module.Core.Audit;
using Sukt.Module.Core.OperationResult;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Sukt.Core.API.Controllers.IdentityServer4
{
    [Description("Api资源管理")]
    public class ApiResourceController : ControllerBase
    {
        private readonly IApiResourceContract _apiResourceContract;
        public ApiResourceController(IApiResourceContract apiResourceContract)
        {
            _apiResourceContract = apiResourceContract;
        }
        /// <summary>
        /// 添加Api资源
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("添加Api资源")]
        [AuditLog]
        public async Task<AjaxResult> CreateAsync([FromBody] ApiResourceInputDto input)
        {
            return (await _apiResourceContract.CreateAsync(input)).ToAjaxResult();
        }
        /// <summary>
        /// 添加Api资源密钥
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("添加Api资源密钥")]
        [AuditLog]
        public async Task<AjaxResult> CreateApiResourceSecretAsync([FromBody] SecretInputDto input)
        {
            return (await _apiResourceContract.CreateApiResourceSecretAsync(input)).ToAjaxResult();
        }
        /// <summary>
        /// 添加Api资源范围
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("添加Api资源范围")]
        [AuditLog]
        public async Task<AjaxResult> CreateApiResourceScopesAsync([FromBody] CommonInputDto input)
        {
            return (await _apiResourceContract.CreateApiResourceScopesAsync(input)).ToAjaxResult();
        }
        /// <summary>
        /// 获取JwtClaimType类型集合
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Description("获取JwtClaimType类型集合")]
        public AjaxResult GetJwtClaimTypeSelectItem()
        {
            return _apiResourceContract.GetJwtClaimTypeSelectItem().ToAjaxResult();
        }
    }
}

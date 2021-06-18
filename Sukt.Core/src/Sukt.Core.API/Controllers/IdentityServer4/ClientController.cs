using Microsoft.AspNetCore.Mvc;
using Sukt.Core.Application.IdentityServer4Contract;
using Sukt.Core.Dtos.IdentityServer4Dto;
using Sukt.Core.Shared;
using Sukt.Module.Core.Audit;
using Sukt.Module.Core.OperationResult;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Sukt.Core.API.Controllers.IdentityServer4
{
    [Description("客户端管理")]
    public class ClientController : ApiControllerBase
    {
        private readonly IClientContract _clientContract;

        public ClientController(IClientContract clientContract)
        {
            _clientContract = clientContract;
        }

        /// <summary>
        /// 添加客户端
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("添加客户端")]
        [AuditLog]
        public async Task<AjaxResult> CreateAsync([FromBody] ClientInputDto input)
        {
            return (await _clientContract.CreateAsync(input)).ToAjaxResult();
        }
        /// <summary>
        /// 获取一个客户端
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Description("获取一个客户端")]
        [AuditLog]
        public async Task<AjaxResult> GetLoadAsync(Guid? id)
        {
            return (await _clientContract.GetLoadAsync(id.Value)).ToAjaxResult();
        }
        /// <summary>
        /// 添加客户端密钥
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("添加客户端密钥")]
        [AuditLog]
        public async Task<AjaxResult> CreateSecretAsync([FromBody] SecretInputDto input)
        {
            return (await _clientContract.CreateSecretAsync(input)).ToAjaxResult();
        }
        /// <summary>
        /// 添加客户端允许访问范围
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("添加客户端允许访问范围")]
        [AuditLog]
        public async Task<AjaxResult> CreateClientScopeAsync([FromBody] CommonInputDto input)
        {
            return (await _clientContract.CreateClientScopeAsync(input)).ToAjaxResult();
        }
        /// <summary>
        /// 添加客户端退出登录Uri
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("添加客户端退出登录Uri")]
        [AuditLog]
        public async Task<AjaxResult> CreatePostLogoutRedirectUriAsync([FromBody] CommonInputDto input)
        {
            return (await _clientContract.CreatePostLogoutRedirectUriAsync(input)).ToAjaxResult();
        }
        /// <summary>
        /// 添加登录回调Uri
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("添加登录回调Uri")]
        [AuditLog]
        public async Task<AjaxResult> CreateRedirectUriAsync([FromBody] CommonInputDto input)
        {
            return (await _clientContract.CreateRedirectUriAsync(input)).ToAjaxResult();
        }
        /// <summary>
        /// 添加允许跨域
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("添加允许跨域")]
        [AuditLog]
        public async Task<AjaxResult> CreateCorsOriginAsync([FromBody] CommonInputDto input)
        {
            return (await _clientContract.CreateCorsOriginAsync(input)).ToAjaxResult();
        }
    }
}

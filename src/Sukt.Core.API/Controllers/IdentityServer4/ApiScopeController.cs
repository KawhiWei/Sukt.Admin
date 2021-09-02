using Microsoft.AspNetCore.Mvc;
using Sukt.Core.Application.IdentityServer4Contract;
using Sukt.Core.Dtos.IdentityServer4Dto.ApiScope;
using Sukt.Module.Core.Audit;
using Sukt.Module.Core.OperationResult;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Sukt.Core.API.Controllers.IdentityServer4
{
    [Description("Api授权访问范围管理")]
    public class ApiScopeController : ControllerBase
    {
        private readonly IApiScopeContract _apiScopeContract;

        public ApiScopeController(IApiScopeContract apiScopeContract)
        {
            _apiScopeContract = apiScopeContract;
        }
        /// <summary>
        /// 添加Api授权访问范围
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("添加Api授权访问范围")]
        [AuditLog]
        public async Task<AjaxResult> CreateAsync([FromBody] List<ApiScopeInputDto> input)
        {
            return (await _apiScopeContract.CreateAsync(input)).ToAjaxResult();
        }
    }
}

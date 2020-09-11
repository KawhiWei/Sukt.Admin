using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using IDN.Services.BasicsService.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sukt.Core.AspNetCore.ApiBase;
using Sukt.Core.Dtos.Function;
using Sukt.Core.Shared.AjaxResult;
using Sukt.Core.Shared.Entity;
using Sukt.Core.Shared.Extensions;
using Sukt.Core.Shared.OperationResult;

namespace Sukt.Core.API.Controllers
{
    /// <summary>
    /// 功能管理
    /// </summary>
    [Description("功能管理")]
    public class FunctionController :  ApiControllerBase
    {
        private readonly IFunctionContract _function;

        public FunctionController(IFunctionContract function)
        {
            _function = function;
        }
        /// <summary>
        /// 创建功能
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("创建功能")]
        public async Task<AjaxResult> CreateAsync([FromBody] FunctionInputDto input)
        {
            return (await _function.InsertAsync(input)).ToAjaxResult();
        }
        /// <summary>
        /// 修改功能
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Description("修改功能")]
        public async Task<AjaxResult> UpdateAsync([FromBody] FunctionInputDto input)
        {
            return (await _function.UpdateAsync(input)).ToAjaxResult();
        }
        /// <summary>
        /// 删除功能
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Description("删除功能")]
        public async Task<AjaxResult> DeleteAsyc(Guid? id)
        {
            return (await _function.DeleteAsync(id.Value)).ToAjaxResult();
        }
        /// <summary>
        /// 异步得到功能分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("异步得到功能分页")]
        public async Task<PageList<FunctionOutputPageDto>> GetFunctionPageAsync([FromBody] PageRequest request)
        {
            return (await _function.GetFunctionPageAsync(request)).PageList();
        }
        /// <summary>
        /// 异步获取功能下拉框列表
        /// </summary>
        /// <returns></returns>
        [Description("异步获取功能下拉框列表")]
        [HttpGet]
        public async Task<AjaxResult> GetFunctionSelectListItemAsync()
        {
            return (await _function.GetFunctionSelectListItemAsync()).ToAjaxResult();
        }
    }
}

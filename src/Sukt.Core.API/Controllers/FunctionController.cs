using Microsoft.AspNetCore.Mvc;
using Sukt.Core.Application;
using Sukt.Core.Dtos.Function;
using Sukt.Core.Shared;
using Sukt.Module.Core.AjaxResult;
using Sukt.Module.Core.Audit;
using Sukt.Module.Core.Entity;
using Sukt.Module.Core.Extensions;
using Sukt.Module.Core.OperationResult;
using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace Sukt.Core.API.Controllers
{
    /// <summary>
    /// 功能管理
    /// </summary>
    [Description("功能管理")]
    public class FunctionController : ApiControllerBase
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
        [AuditLog]
        public async Task<AjaxResult> CreateAsync([FromBody] FunctionInputDto input)
        {
            return (await _function.InsertAsync(input)).ToAjaxResult();
        }

        /// <summary>
        /// 修改功能
        /// </summary>
        /// <param name="input"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Description("修改功能")]
        [AuditLog]
        public async Task<AjaxResult> UpdateAsync(Guid id, [FromBody] FunctionInputDto input)
        {
            return (await _function.UpdateAsync(id,input)).ToAjaxResult();
        }
        /// <summary>
        /// 根据Id加载表单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Description("加载表单")]
        public async Task<AjaxResult> LoadFormAsync(Guid id)
        {
            Console.WriteLine($"控制器执行线程：{Thread.CurrentThread.ManagedThreadId}");
            _function.TestA();
            var result = await _function.LoadFromAsync(id).ConfigureAwait(false);
            Console.WriteLine(result.Message);
            _function.TestB();
            return result.ToAjaxResult();
        }

        /// <summary>
        /// 删除功能
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Description("删除功能")]
        [AuditLog]
        public async Task<AjaxResult> DeleteAsync(Guid id)
        {
            return (await _function.DeleteAsync(id)).ToAjaxResult();
        }

        /// <summary>
        /// 异步得到功能分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("异步得到功能分页")]
        public async Task<PageList<FunctionOutputPageDto>> GetPageAsync([FromBody] PageRequest request)
        {
            return (await _function.GetPageAsync(request)).PageList();
        }
    }
}

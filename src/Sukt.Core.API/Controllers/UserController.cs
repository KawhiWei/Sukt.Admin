using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sukt.Core.Application;
using Sukt.Core.Shared;
using Sukt.Core.Dtos;
using Sukt.Module.Core.Audit;
using Sukt.Module.Core.OperationResult;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Sukt.Core.API.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Description("用户管理")]
    public class UserController : ApiControllerBase
    {
        private readonly IUserContract _userContract;
        private readonly ILogger<UserController> _logger = null;

        public UserController(IUserContract userContract, ILogger<UserController> logger)
        {
            _userContract = userContract;
            _logger = logger;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("添加用户")]
        [AuditLog]
        public async Task<AjaxResult> CreateAsync([FromBody] UserInputDto input)
        {
            return (await _userContract.InsertAsync(input)).ToAjaxResult();
        }

        /// <summary>
        /// 根据Id加载表单用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Description("加载表单用户")]
        [HttpGet("id")]
        public async Task<AjaxResult> LoadUserFormAsync(Guid id)
        {
            return (await _userContract.LoadUserFormAsync(id)).ToAjaxResult();
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="input"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Description("修改用户")]
        [AuditLog]
        public async Task<AjaxResult> UpdateAsync(Guid id,[FromBody] UserInputDto input)
        {
            return (await _userContract.UpdateAsync(id,input)).ToAjaxResult();
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Description("删除用户")]
        [AuditLog]
        public async Task<AjaxResult> DeleteAsync(Guid id)
        {
            return (await _userContract.DeleteAsync(id)).ToAjaxResult();
        }
    }
}

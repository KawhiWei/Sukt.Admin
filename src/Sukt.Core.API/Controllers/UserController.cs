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
using Sukt.Module.Core.AjaxResult;
using Sukt.Module.Core.Entity;
using Sukt.Module.Core.Extensions;

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
        [HttpGet("{id}")]
        public async Task<AjaxResult> LoadFormAsync(Guid id)
        {
            return (await _userContract.LoadFormAsync(id)).ToAjaxResult();
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
        /// 用户分页接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("用户分页接口")]
        public async Task<PageList<UserPageOutputDto>> GetPageAsync([FromBody] PageRequest request)
        {
            return (await _userContract.GetPageAsync(request)).PageList();
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

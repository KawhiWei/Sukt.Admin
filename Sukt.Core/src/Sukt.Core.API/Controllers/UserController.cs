using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sukt.Core.Application;
using Sukt.Core.AspNetCore.ApiBase;
using Sukt.Core.Dtos;
using Sukt.Core.Shared.OperationResult;

namespace Sukt.Core.API.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Description("用户管理")]
    [ApiController]
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
        [HttpGet]
        public async Task<AjaxResult> LoadUserFormAsync(Guid? id)
        {
            return (await _userContract.LoadUserFormAsync(id.Value)).ToAjaxResult();
        }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Description("修改用户")]
        public async Task<AjaxResult> UpdateAsync([FromBody] UserUpdateInputDto input)
        {
            return (await _userContract.UpdateAsync(input)).ToAjaxResult();
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Description("删除用户")]
        public async Task<AjaxResult> DeleteAsync(Guid? id)
        {
            return (await _userContract.DeleteAsync(id.Value)).ToAjaxResult();
        }
    }
}

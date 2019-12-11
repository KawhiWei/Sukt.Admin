using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Uwl.Data.Model.Result;
using Uwl.Data.Model.VO.Personal;
using Uwl.Data.Server.UserServices;

namespace UwlAPI.Tools.Controllers
{
    /// <summary>
    /// 个人资料修改控制器
    /// </summary>
    [Route("api/Personal")]
    [ApiController]
    //[EnableCors("AllRequests")]
    public class PersonalCenterController : BaseController<PersonalCenterController>
    {
        private IUserServer _userserver;
        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="userServer"></param>
        /// <param name="logger"></param>
        public PersonalCenterController(IUserServer userServer, ILogger<PersonalCenterController> logger
            ) : base(logger)
        {
            this._userserver = userServer;
        }
        /// <summary>
        /// 修改密码接口
        /// </summary>
        /// <param name="changePwd"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ChangePassword")]
        public async Task<MessageModel<string>> ChangePassword([FromBody]ChangePwdVO changePwd)
        {
            var data = new MessageModel<string>();

            if (changePwd.UserId == Guid.Empty)
            {
                changePwd.UserId = UserId.Value;
            }
            data.success = await _userserver.ChangePwd(changePwd);
            if (data.success)
            {
                data.msg = "密码修改成功";
            }
            else
            {
                data.msg = "密码修改失败";
            }
            return data;

        }
        /// <summary>
        /// 修改密码接口
        /// </summary>
        /// <param name="changeData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ChangeDatas")]
        public async Task<MessageModel<string>> ChangeData([FromBody]ChangeDataVO changeData)
        {
            var data = new MessageModel<string>();
            if (changeData.UserId == Guid.Empty)
            {
                changeData.UserId = UserId.Value;
            }
            data.success = await _userserver.ChangeData(changeData);
            if (data.success)
            {
                data.msg = "密码修改成功";
            }
            else
            {
                data.msg = "密码修改失败";
            }
            return data;

        }
    }
}
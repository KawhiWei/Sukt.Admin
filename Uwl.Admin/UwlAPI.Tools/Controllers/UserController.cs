using System.Collections.Generic;
using System.Linq;
using UwlAPI.Tools.Models.LoginViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Uwl.Data.Model.BaseModel;
using Uwl.Data.Server.UserServices;
using System.Threading.Tasks;
using System;
using Uwl.Data.Model.Result;
using Uwl.Extends.Utility;
using UwlAPI.Tools.AuthHelper.Token;
using Uwl.Data.Model.Assist;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Uwl.Common.Download;
using Uwl.Common.Helper;
using Uwl.Extends.EncryPtion;
using Microsoft.Extensions.Logging;

namespace UwlAPI.Tools.Controllers
{

    /// <summary>
    /// 用户管理API接口
    /// </summary>
    //浏览器访问API控制器接口[Route("test/getcontent")]
    //[Authorize(Policy = "Admin")]
    //[Authorize("Permission")]
    //[EnableCors("AllRequests")]
    [Route("api/User")]
    [ApiController]
    //[EnableCors("AllRequests")]
    //[AllowAnonymous]//允许匿名访问
    public class UserController : BaseController<UserController>
    {
        /// <summary>
        /// 创建一个服务层的对象来进行服务层的方法调用
        /// </summary>
        private IUserServer _userserver;
        /// <summary>
        /// 注入服务层
        /// </summary>
        /// <param name="userServer"></param>
        /// <param name="logger"></param>
        public UserController(IUserServer userServer, ILogger<UserController> logger) : base(logger)
        {
            _userserver = userServer;
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]//请求模式
        [Route("UserInfo")]
        public async Task<MessageModel<SysUser>> GetUser(string token)
        {
            var ss = HttpContext.User.Claims;
            var data = new MessageModel<SysUser>();
            if (!token.IsNullOrEmpty())
            {
                //解析Token字符串，获取到用户ID
                var tokenModel = JwtHelper.SerializeJwt(token);
                if (tokenModel != null && tokenModel.Uid != Guid.Empty)
                {
                    //根据用户ID获取用户信息
                    var userInfo = await _userserver.GetSysUser(tokenModel.Uid);
                    if (userInfo != null)
                    {
                        data.response = userInfo;
                        data.success = true;
                        data.msg = "用户信息获取成功";
                    }
                }
            }
            return data;

        }
        /// <summary>
        /// 分页获取用户信息
        /// </summary>
        /// <param name="userQuery"></param>
        /// <returns></returns>
        [Route("PageUser")]
        [HttpGet]
        public MessageModel<PageModel<SysUser>> GetUserByPage([FromQuery]UserQuery userQuery)
        {
            var list = _userserver.GetUserListByPage(userQuery, out int Total);
            return new MessageModel<PageModel<SysUser>>()
            {
                success = true,
                msg = "数据获取成功",
                response = new PageModel<SysUser>()
                {
                    TotalCount = Total,
                    data = list.ToList(),
                }
            };
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        [Route("AddUser")]
        [HttpPost]
        public async Task<MessageModel<string>> AddUsers([FromBody] SysUser sysUser)
        {
            var Pwd = Appsettings.app(new string[] { "DefaultPwd", "password" });
            var data = new MessageModel<string>();
            sysUser.Password = Pwd.ToMD5();
            data.success = await _userserver.AddUser(sysUser);
            if (data.success)
            {
                data.msg = $"用户添加成功:默认密码：【{Pwd}】";
            }
            return data;
        }
        /// <summary>
        /// 用户信息修改
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        [Route("UpdateUser")]
        [HttpPut]
        public async Task<MessageModel<string>> UpdateUser([FromBody] SysUser sysUser)
        {
            var data = new MessageModel<string>();
            data.success = await _userserver.UpdateUser(sysUser);
            if (data.success)
            {
                data.msg = "用户修改成功";
            }
            return data;
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        [Route("DeleteUser")]
        [HttpDelete]
        public async Task<MessageModel<string>> DeleteUser(string Ids)
        {
            var list = JsonConvert.DeserializeObject<List<Guid>>(Ids);
            var data = new MessageModel<string>();
            data.success = await _userserver.DeleteUser(list);
            if (data.success)
            {
                data.msg = "删除成功";
            }
            return data;
        }
        /// <summary>
        /// 上传Excel文件
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpPost]
        //[Consumes("application/json", "multipart/form-data")]
        [Route("UpLoad")]
        public MessageModel<string> UploadUser()
        {
            var data = new MessageModel<string>();
            var files = Request.Form.Files.First();
            if (files == null)
            {
                data.msg = "未找到上传文件";
            }
            //var list = ExcelHelper<SysUser>.UpLoad(files, 0);
            //foreach (var item in list)
            //{

            //}
            data.msg = "用户信息导入成功";
            data.success = true;
            return data;
        }
        /// <summary>
        /// 员工信息导出Excel
        /// </summary>
        /// <param name="userQuery">根据条件导出</param>
        /// <returns></returns>
        //[AllowAnonymous]
        [Route("DownLoad")]
        [HttpGet]
        public IActionResult ExcelDown([FromQuery]UserQuery userQuery)
        {
            var List = _userserver.GetUserListByPage(userQuery, out int total);
            //byte[] fileContent = ExcelHelper<SysUser>.ToExcel(List);
            return File("", "application/vnd.ms-excel", $"{DateTime.Now.ToString("yyyyMMddhhmmss")}.xlsx");//charset='utf-8'
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Uwl.Data.Model.MenuViewModel;
using Uwl.Data.Model.Result;
using Uwl.Data.Model.RoleAssigVO;
using Uwl.Data.Server.RoleAssigServices;

namespace UwlAPI.Tools.Controllers
{
    /// <summary>
    /// 角色分配权限控制器
    /// </summary>
    [Route("api/RoleAssig")]
    [ApiController]
    //[EnableCors("AllRequests")]
    public class RoleAssigController : BaseController<RoleAssigController>
    {
        private IRoleAssigServer _roleAssigServer;
        /// <summary>
        /// 角色权限接口
        /// </summary>
        /// <param name="roleAssigServer"></param>
        /// <param name="logger">日志记录</param>
        public RoleAssigController(IRoleAssigServer roleAssigServer, ILogger<RoleAssigController> logger) : base(logger)
        {
            _roleAssigServer = roleAssigServer;
        }
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        /// <summary>
        /// 根据角色ID获取所有的菜单和按钮（包含已分配或者未分配的）
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [Route("GetRoleAssigTree")]
        [HttpGet]
        public async Task<MessageModel<RoleAssigMenuViewModel>> GetRoleAssig([FromQuery] Guid roleId)
        {
            var data = new MessageModel<RoleAssigMenuViewModel>();
            var RoleAssigMenuView = await _roleAssigServer.GetRoleAssigMenuViewModels(roleId);
            data.success = true;
            data.response = RoleAssigMenuView;
            data.msg = "角色权限获取成功";
            return data;

        }
        /// <summary>
        /// 保存角色权限
        /// </summary>
        /// <param name="saveRoleAssigView">数据保存对象</param>
        [Route("SaveRoleAssig")]
        [HttpPost]
        public async Task<MessageModel<string>> Save([FromBody] SaveRoleAssigViewModel saveRoleAssigView)
        {
            var data = new MessageModel<string>();

            data.success = await _roleAssigServer.SaveRoleAssig(saveRoleAssigView);
            if (data.success)
            {
                data.msg = "权限修改成功";
                return data;
            }
            return data;

        }
    }
}

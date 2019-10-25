using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class RoleAssigController : ControllerBase
    {
        private IRoleAssigServer _roleAssigServer;
        /// <summary>
        /// 角色权限接口
        /// </summary>
        /// <param name="roleAssigServer"></param>
        public RoleAssigController(IRoleAssigServer roleAssigServer)
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
        public async  Task<MessageModel<RoleAssigMenuViewModel>> GetRoleAssig([FromQuery] Guid roleId)
        {
            var data = new MessageModel<RoleAssigMenuViewModel>();
            try
            {
                var RoleAssigMenuView = await _roleAssigServer.GetRoleAssigMenuViewModels(roleId);
                data.success = true;
                data.response = RoleAssigMenuView;
                data.msg = "角色权限获取成功";
                return data;
            }
            catch (Exception ex)
            {
                data.msg = "获取数据失败" + ex.Message;
                return data;
            }
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
            try
            {
                data.success = await _roleAssigServer.SaveRoleAssig(saveRoleAssigView);
                if (data.success)
                {
                    data.msg = "权限修改成功";
                    return data;
                }
                return data;
            }
            catch (Exception ex)
            {
                data.msg += ex.Message;
                return data;
            }
        }
    }
}

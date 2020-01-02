using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Uwl.Common.GlobalRoute;
using Uwl.Data.Model.Assist;
using Uwl.Data.Model.BaseModel;
using Uwl.Data.Model.Result;
using Uwl.Data.Server.RoleServices;

namespace UwlAPI.Tools.Controllers
{
    /// <summary>
    /// 角色管理API接口
    /// </summary>
    [Route("api/Roles")]
    [ApiController]
    //[EnableCors("AllRequests")]
    [Authorize(GlobalRouteAuthorizeVars.Name)]
    public class RoleController : BaseController<RoleController>
    {
        private IRoleServer _roleServer;
        /// <summary>
        /// 角色管理接口
        /// </summary>
        /// <param name="roleServer"></param>
        /// <param name="logger">日志记录</param>
        public RoleController(IRoleServer roleServer, ILogger<RoleController> logger) : base(logger)
        {
            _roleServer = roleServer;
        }
        /// <summary>
        /// 分页获取角色
        /// </summary>
        /// <param name="roleQuery">查询条件</param>
        /// <returns></returns>
        [Route("PageByRole")]
        [HttpGet]
        public MessageModel<PageModel<SysRole>> GetUserByPage([FromQuery]RoleQuery roleQuery)
        {
            var list = _roleServer.GetRoleListByPage(roleQuery, out int Total);
            return new MessageModel<PageModel<SysRole>>()
            {
                success = true,
                msg = "数据获取成功",
                response = new PageModel<SysRole>()
                {
                    TotalCount = Total,
                    data = list.ToList(),
                }
            };
        }
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="sysRole"></param>
        /// <returns></returns>
        [Route("AddRole")]
        [HttpPost]
        public async Task<MessageModel<string>> AddRole([FromBody] SysRole sysRole)
        {
            var data = new MessageModel<string>();
            data.success = await _roleServer.AddRole(sysRole);
            if (data.success)
            {
                data.msg = "角色添加成功";
            }
            return data;
        }
        /// <summary>
        /// 角色信息修改
        /// </summary>
        /// <param name="sysRole"></param>
        /// <returns></returns>
        [Route("UpdateRole")]
        [HttpPut]
        public async Task<MessageModel<string>> UpdateRole([FromBody] SysRole sysRole)
        {
            var data = new MessageModel<string>();
            data.success = await _roleServer.UpdateRole(sysRole);
            if (data.success)
            {
                data.msg = "角色修改成功";
            }
            return data;
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        [Route("DeleteRole")]
        [HttpDelete]
        public async Task<MessageModel<string>> DeleteUser(string Ids)
        {
            var list = JsonConvert.DeserializeObject<List<Guid>>(Ids);
            var data = new MessageModel<string>();

            data.success = await _roleServer.DeleteRole(list);
            if (data.success)
            {
                data.msg = "删除成功";
            }
            return data;

        }
        /// <summary>
        /// 获取所有的角色列表
        /// </summary>
        /// <returns></returns>
        [Route("GetAllRole")]
        [HttpGet]
        public async Task<MessageModel<PageModel<SysRole>>> GetAllRole()
        {
            var list = await _roleServer.GetAllListByWhere();
            return new MessageModel<PageModel<SysRole>>()
            {
                success = true,
                msg = "数据获取成功",
                response = new PageModel<SysRole>()
                {
                    TotalCount = list.Count,
                    data = list,
                }
            };
        }
    }
}
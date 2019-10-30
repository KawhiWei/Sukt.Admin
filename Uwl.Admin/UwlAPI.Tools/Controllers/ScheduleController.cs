using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Uwl.Data.Model.BaseModel;
using Uwl.Data.Model.Result;
using Uwl.Data.Server.ScheduleServices;

namespace UwlAPI.Tools.Controllers
{
    /// <summary>
    /// 任务计划管理API接口
    /// </summary>
    [Route("api/Schedule")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleServer _scheduleServer;//计划任务管理服务层
        /// <summary>
        /// 注入构造函数
        /// </summary>
        public ScheduleController(IScheduleServer scheduleServer)
        {
            _scheduleServer = scheduleServer;
        }
        /// <summary>
        /// 添加计划任务
        /// </summary>
        /// <param name="sysRole"></param>
        /// <returns></returns>
        [Route("AddSchedule")]
        [HttpPost]
        public async Task<MessageModel<string>> AddSchedule([FromBody] SysSchedule sysRole)
        {
            var data = new MessageModel<string>();
            data.success = await _scheduleServer.AddScheduleAsync(sysRole);
            if (data.success)
            {
                data.msg = "角色添加成功";
            }
            return data;
        }
        /// <summary>
        /// 修改计划任务
        /// </summary>
        /// <param name="sysRole"></param>
        /// <returns></returns>
        [Route("UpdateSchedule")]
        [HttpPut]
        public async Task<MessageModel<string>> UpdateSchedule([FromBody] SysSchedule sysRole)
        {
            var data = new MessageModel<string>();
            data.success = await _scheduleServer.UpdateScheduleAsync(sysRole);
            if (data.success)
            {
                data.msg = "角色修改成功";
            }
            return data;
        }
    }
}
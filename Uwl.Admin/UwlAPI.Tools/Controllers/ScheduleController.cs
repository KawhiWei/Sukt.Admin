using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Uwl.Common.GlobalRoute;
using Uwl.Data.Model.Assist;
using Uwl.Data.Model.BaseModel;
using Uwl.Data.Model.Result;
using Uwl.Data.Server.ScheduleServices;
using Uwl.Extends.Utility;
using Uwl.QuartzNet.JobCenter.Result;

namespace UwlAPI.Tools.Controllers
{
    /// <summary>
    /// 任务计划管理API接口
    /// </summary>
    [Route("api/Schedule")]
    [ApiController]
    //[EnableCors("AllRequests")]
    [Authorize(GlobalRouteAuthorizeVars.Name)]
    public class ScheduleController : BaseController<ScheduleController>
    {
        private readonly IScheduleServer _scheduleServer;//计划任务管理服务层
        /// <summary>
        /// 注入构造函数
        /// </summary>
        /// <param name="scheduleServer">服务层注入</param>
        /// <param name="logger">日志记录</param>
        public ScheduleController(IScheduleServer scheduleServer, ILogger<ScheduleController> logger) : base(logger)
        {
            _scheduleServer = scheduleServer;
        }
        /// <summary>
        /// 分页获取按钮
        /// </summary>
        /// <param name="scheduleQuery">查询方法</param>
        /// <returns></returns>
        [Route("PageJob")]
        // GET: api/Buttons/5
        [HttpGet]
        public MessageModel<PageModel<SysSchedule>> GetSchedulePage([FromQuery]ScheduleQuery scheduleQuery)
        {
            var list = _scheduleServer.GetScheduleJobByPage(scheduleQuery);
            return new MessageModel<PageModel<SysSchedule>>()
            {
                success = true,
                msg = "数据获取成功",
                response = new PageModel<SysSchedule>()
                {
                    TotalCount = list.Item2,
                    data = list.Item1,
                }
            };


        }
        /// <summary>
        /// 添加计划任务
        /// </summary>
        /// <param name="sysSchedule"></param>
        /// <returns></returns>
        [Route("AddSchedule")]
        [HttpPost]
        public async Task<MessageModel<string>> AddSchedule([FromBody] SysSchedule sysSchedule)
        {
            var data = new MessageModel<string>();

            data.success = await _scheduleServer.AddScheduleAsync(sysSchedule);
            if (data.success)
            {
                data.msg = "任务添加成功";
            }
            else
            {
                data.msg = "任务添加失败";
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
                data.msg = "任务修改成功";
            }
            else
            {
                data.msg = "任务修改失败";
            }

            return data;
        }
        /// <summary>
        /// 启动计划任务
        /// </summary>
        /// <param name="JobId"></param>
        /// <returns></returns>
        [Route("StartJob")]
        [HttpGet]
        public async Task<MessageModel<string>> StartJob(Guid JobId)
        {
            var data = new MessageModel<string>();

            var Resultmodel = await _scheduleServer.StartJob(JobId);
            data.success = Resultmodel.IsSuccess;
            data.msg = Resultmodel.Message;
            return data;

        }
        /// <summary>
        /// 停止一个计划任务
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        [Route("StopJob")]
        [HttpGet]
        public async Task<MessageModel<string>> StopJob(Guid jobId)
        {
            var data = new MessageModel<string>();

            var Resultmodel = await _scheduleServer.StopJob(jobId);
            data.success = Resultmodel.IsSuccess;
            data.msg = Resultmodel.Message;
            return data;

        }
        /// <summary>
        /// 停止一个计划任务
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns></returns>
        [Route("ReCovery")]
        [HttpGet]
        public async Task<MessageModel<string>> ReCovery(Guid jobId)
        {
            var data = new MessageModel<string>();
            var Resultmodel = await _scheduleServer.ReCoveryJob(jobId);
            data.success = Resultmodel.IsSuccess;
            data.msg = Resultmodel.Message;
            return data;

        }
    }
}
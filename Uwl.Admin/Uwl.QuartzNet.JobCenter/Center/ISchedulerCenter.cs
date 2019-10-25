using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Uwl.Data.Model.BaseModel;
using Uwl.QuartzNet.JobCenter.Result;

namespace Uwl.QuartzNet.JobCenter.Center
{
    public interface ISchedulerCenter
    {

        /// <summary>
        /// 开启任务调度
        /// </summary>
        /// <returns></returns>
        Task<JobResuleModel> StartScheduleAsync();
        /// <summary>
        /// 停止任务调度
        /// </summary>
        /// <returns></returns>
        Task<JobResuleModel> StopScheduleAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sysSchedule"></param>
        /// <returns></returns>
        Task<JobResuleModel> AddScheduleJobAsync(SysSchedule sysSchedule);

    }
}

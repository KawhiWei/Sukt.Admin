using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Uwl.Data.Model.Assist;
using Uwl.Data.Model.BaseModel;
using Uwl.QuartzNet.JobCenter.Result;

namespace Uwl.Data.Server.ScheduleServices
{
    public interface IScheduleServer
    {
        /// <summary>
        /// 分页获取任务计划列表
        /// </summary>
        /// <param name="scheduleQuery"></param>
        /// <returns></returns>
        (List<SysSchedule>, int) GetScheduleJobByPage(ScheduleQuery scheduleQuery);
        /// <summary>
        /// 添加计划任务异步等待
        /// </summary>
        /// <param name="sysRole"></param>
        /// <returns></returns>
        Task<bool> AddScheduleAsync(SysSchedule sysSchedule);
        /// <summary>
        /// 修改计划任务异步等待
        /// </summary>
        /// <param name="sysRole"></param>
        /// <returns></returns>
        Task<bool> UpdateScheduleAsync(SysSchedule sysSchedule);
        /// <summary>
        /// 修改计划任务
        /// </summary>
        /// <param name="sysSchedule"></param>
        /// <returns></returns>
        bool UpdateSchedule(SysSchedule sysSchedule);
        /// <summary>
        /// 添加计划任务
        /// </summary>
        /// <param name="sysRole"></param>
        /// <returns></returns>
        bool AddSchedule(SysSchedule sysSchedule);
        /// <summary>
        /// 根据Id查询出一个Job任务实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<JobResuleModel> StartJob(Guid Id);
        /// <summary>
        /// 暂停一个任务
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<JobResuleModel> StopJob(Guid Id);
    }
}

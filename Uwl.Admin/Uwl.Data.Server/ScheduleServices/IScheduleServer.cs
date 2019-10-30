using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Uwl.Data.Model.BaseModel;

namespace Uwl.Data.Server.ScheduleServices
{
    public interface IScheduleServer
    {
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
    }
}

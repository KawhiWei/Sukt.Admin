using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Uwl.Data.Model.BaseModel;
using Uwl.Domain.ScheduleInterface;

namespace Uwl.Data.Server.ScheduleServices
{
    public class ScheduleServer: IScheduleServer
    {
        private readonly IScheduleRepositoty _scheduleRepositoty;
        public ScheduleServer(IScheduleRepositoty scheduleRepositoty)
        {
            _scheduleRepositoty = scheduleRepositoty;
        }

        public bool AddSchedule(SysSchedule sysSchedule)
        {
            return this._scheduleRepositoty.Insert(sysSchedule);
        }

        public async Task<bool> AddScheduleAsync(SysSchedule sysSchedule)
        {
            return await this._scheduleRepositoty.InsertAsync(sysSchedule);
        }

        public bool UpdateSchedule(SysSchedule sysSchedule)
        {
            sysSchedule.UpdateDate = DateTime.Now;
            return  _scheduleRepositoty.UpdateNotQuery(sysSchedule,
                x => x.Name, x => x.JobGroup, x => x.Cron, x => x.UpdateDate, x => x.UpdateName, x => x.UpdateId
                , x => x.AssemblyName, x => x.ClassName, x => x.Remark, x => x.RunTimes, x => x.BeginTime, x => x.BeginTime,
                x => x.EndTime, x => x.TriggerType, x => x.IntervalSecond, x => x.IsStart) > 0;
        }

        public async Task<bool> UpdateScheduleAsync(SysSchedule sysSchedule)
        {
            sysSchedule.UpdateDate = DateTime.Now;
            return await _scheduleRepositoty.UpdateNotQueryAsync(sysSchedule, 
                x => x.Name, x => x.JobGroup, x => x.Cron, x => x.UpdateDate, x => x.UpdateName, x => x.UpdateId
                , x => x.AssemblyName, x => x.ClassName, x => x.Remark, x => x.RunTimes, x => x.BeginTime, x => x.BeginTime, 
                x => x.EndTime, x => x.TriggerType, x => x.IntervalSecond, x => x.IsStart) > 0;
        }
    }
}

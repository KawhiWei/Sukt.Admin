using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uwl.Data.Model.Assist;
using Uwl.Data.Model.BaseModel;
using Uwl.Data.Server.LambdaTree;
using Uwl.Domain.ScheduleInterface;
using Uwl.QuartzNet.JobCenter.Center;
using Uwl.QuartzNet.JobCenter.Result;

namespace Uwl.Data.Server.ScheduleServices
{
    public class ScheduleServer: IScheduleServer
    {
        private readonly IScheduleRepositoty _scheduleRepositoty;
        private readonly ISchedulerCenter _schedulerCenter;
        public ScheduleServer(IScheduleRepositoty scheduleRepositoty, ISchedulerCenter schedulerCenter)
        {
            this._scheduleRepositoty = scheduleRepositoty;
            this._schedulerCenter = schedulerCenter;
        }

        public (List<SysSchedule>,int) GetScheduleJobByPage(ScheduleQuery scheduleQuery)
        {
            var query = ExpressionBuilder.True<SysSchedule>();
            int Total = _scheduleRepositoty.Count(query);
            var list = _scheduleRepositoty.PageBy(scheduleQuery.PageIndex, scheduleQuery.PageSize, query).ToList();
            return (list,Total);
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
        /// <summary>
        /// 根据Id查询出一个Job任务实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<JobResuleModel> StartJob(Guid Id)
        {
            var model=await _scheduleRepositoty.FirstOrDefaultAsync(x => x.Id == Id);
            var ResuleModel=await _schedulerCenter.AddScheduleJobAsync(model);
            if (ResuleModel.IsSuccess)
                model.IsStart = true;
            await UpdateScheduleAsync(model);
            return ResuleModel;
        }
        /// <summary>
        /// 根据Id查询出一个Job任务实体
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<JobResuleModel> StopJob(Guid Id)
        {
            var model = await _scheduleRepositoty.FirstOrDefaultAsync(x => x.Id == Id);
            var ResuleModel = await _schedulerCenter.StopScheduleJobAsync(model);
            if (ResuleModel.IsSuccess)
                model.IsStart =false;    
            await UpdateScheduleAsync(model);
            return ResuleModel;
        }
    }
}

using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Uwl.Data.Model.BaseModel;
using Uwl.Extends.Utility;
using Uwl.QuartzNet.JobCenter.Result;

namespace Uwl.QuartzNet.JobCenter.Center
{
    /// <summary>
    /// 任务调度管理中心
    /// </summary>
    public class SchedulerCenterServer: ISchedulerCenter
    {
        private Task<IScheduler> _scheduler;
        public SchedulerCenterServer()
        {
            _scheduler = GetSchedulerAsync();
        }
        private Task<IScheduler> GetSchedulerAsync()
        {
            if (_scheduler != null)
                return this._scheduler;
            else
            {
                // 从Factory中获取Scheduler实例
                NameValueCollection collection = new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" },
                };
                StdSchedulerFactory factory = new StdSchedulerFactory(collection);
                return _scheduler= factory.GetScheduler();
            }
        }

        /// <summary>
        /// 开启任务调度
        /// </summary>
        /// <returns></returns>
        public async Task<JobResuleModel> StartScheduleAsync()
        {
            var result = new JobResuleModel();
            try
            {
                if (!this._scheduler.Result.IsStarted)
                {
                    //等待任务运行完成
                    await this._scheduler.Result.Start();
                    await Console.Out.WriteLineAsync("任务调度开启！");
                    result.IsSuccess = true;
                    result.Message = $"任务调度开启成功";
                    return result;
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = $"任务调度已经开启";
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 停止任务调度
        /// </summary>
        /// <returns></returns>
        public async Task<JobResuleModel> StopScheduleAsync()
        {
            var result = new JobResuleModel();
            try
            {
                if (!this._scheduler.Result.IsShutdown)
                {
                    //等待任务运行完成
                    await this._scheduler.Result.Shutdown();
                    await Console.Out.WriteLineAsync("任务调度停止！");
                    result.IsSuccess = true;
                    result.Message = $"任务调度停止成功";
                    return result;
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = $"任务调度已经停止";
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 添加一个计划任务（映射程序集指定IJob实现类）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sysSchedule"></param>
        /// <returns></returns>
        public async Task<JobResuleModel> AddScheduleJobAsync(SysSchedule sysSchedule)
        {
            var result = new JobResuleModel();
            try
            {
                if (sysSchedule != null)
                {
                    JobKey jobKey = new JobKey(sysSchedule.Name, sysSchedule.JobGroup);
                    //if(_scheduler.Result.CheckExists(jobKey))

                    #region 设置开始时间和结束时间

                    if (sysSchedule.BeginTime == null)
                    {
                        sysSchedule.BeginTime = DateTime.Now;
                    }
                    DateTimeOffset starRunTime = DateBuilder.NextGivenSecondDate(sysSchedule.BeginTime, 1);//设置开始时间
                    if (sysSchedule.EndTime == null)
                    {
                        sysSchedule.EndTime = DateTime.MaxValue.AddDays(-1);
                    }
                    DateTimeOffset endRunTime = DateBuilder.NextGivenSecondDate(sysSchedule.EndTime, 1);//设置暂停时间

                    #endregion

                    #region 通过反射获取程序集类型和类   
                    
                    Assembly assembly = Assembly.Load(new AssemblyName(sysSchedule.AssemblyName));
                    Type jobType = assembly.GetType(sysSchedule.AssemblyName + "." + sysSchedule.ClassName);

                    #endregion
                    //判断任务调度是否开启
                    if(!_scheduler.Result.IsStarted)
                    {
                        await StartScheduleAsync();
                    }

                    //传入反射出来的执行程序集
                    IJobDetail job = new JobDetailImpl(sysSchedule.Name, sysSchedule.JobGroup, jobType);
                    //Job执行时的参数还有待解决？？？？？？？？？？？？？？？？？？？？？
                    ITrigger trigger;

                    #region 泛型传递
                    //IJobDetail job = JobBuilder.Create<T>()
                    //    .WithIdentity(sysSchedule.Name, sysSchedule.JobGroup)
                    //    .Build();
                    #endregion

                    if (!sysSchedule.Cron.IsNullOrEmpty()&& CronExpression.IsValidExpression(sysSchedule.Cron))
                    {
                        trigger = CreateCronTrigger(sysSchedule);
                    }
                    else
                    {
                        trigger = CreateSimpleTrigger(sysSchedule);
                    }
                    // 告诉Quartz使用我们的触发器来安排作业
                    await _scheduler.Result.ScheduleJob(job, trigger);
                    //await Task.Delay(TimeSpan.FromSeconds(120));
                    //await Console.Out.WriteLineAsync("关闭了调度器！");
                    //await _scheduler.Result.Shutdown();
                    result.IsSuccess = true;
                    result.Message = $"暂停任务:【{sysSchedule.Name}】成功";
                    return result;
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = $"任务计划不存在:【{sysSchedule.Name}】";
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 暂停一个指定的计划任务
        /// </summary>
        /// <returns></returns>
        public async Task<JobResuleModel> StopScheduleJobAsync(SysSchedule sysSchedule)
        {
            var result = new JobResuleModel();
            try
            {
                JobKey jobKey = new JobKey(sysSchedule.Name, sysSchedule.JobGroup);
                if (!await _scheduler.Result.CheckExists(jobKey))
                {
                    result.IsSuccess = false;
                    result.Message = $"未找到要暂停的任务:【{sysSchedule.Name}】";
                    return result;
                }
                else
                {
                    await this._scheduler.Result.PauseJob(jobKey);
                    result.IsSuccess = true;
                    result.Message = $"暂停任务:【{sysSchedule.Name}】成功";
                    return result;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 恢复指定的计划任务
        /// </summary>
        /// <param name="sysSchedule"></param>
        /// <returns></returns>
        public async Task<JobResuleModel> ResumeJob(SysSchedule sysSchedule)
        {
            var result = new JobResuleModel();
            try
            {
                JobKey jobKey = new JobKey(sysSchedule.Name, sysSchedule.JobGroup);
                if (!await _scheduler.Result.CheckExists(jobKey))
                {
                    result.IsSuccess = false;
                    result.Message = $"未找到要重新的任务:【{sysSchedule.Name}】,请先选择添加计划！";
                    return result;
                }
                await this._scheduler.Result.ResumeJob(jobKey);
                result.IsSuccess = true;
                result.Message = $"恢复计划任务:【{sysSchedule.Name}】成功";
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region 创建触发器帮助方法

        /// <summary>
        /// 创建SimpleTrigger触发器（简单触发器）
        /// </summary>
        /// <param name="sysSchedule"></param>
        /// <param name="starRunTime"></param>
        /// <param name="endRunTime"></param>
        /// <returns></returns>
        private ITrigger CreateSimpleTrigger(SysSchedule sysSchedule)
        {
            if(sysSchedule.RunTimes>0)
            {
                ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(sysSchedule.Name, sysSchedule.JobGroup)
                .StartAt(sysSchedule.BeginTime.Value)
                .EndAt(sysSchedule.EndTime.Value)
                .WithSimpleSchedule(x =>
                x.WithIntervalInSeconds(sysSchedule.IntervalSecond)
                .WithRepeatCount(sysSchedule.RunTimes)).ForJob(sysSchedule.Name,sysSchedule.JobGroup).Build();
                return trigger;
            }
            else
            {
                ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(sysSchedule.Name, sysSchedule.JobGroup)
                .StartAt(sysSchedule.BeginTime.Value)
                .EndAt(sysSchedule.EndTime.Value)
                .WithSimpleSchedule(x =>
                x.WithIntervalInSeconds(sysSchedule.IntervalSecond)
                .RepeatForever()).ForJob(sysSchedule.Name, sysSchedule.JobGroup).Build();
                return trigger;
            }
            // 触发作业立即运行，然后每10秒重复一次，无限循环
            
        }
        /// <summary>
        /// 创建类型Cron的触发器
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        private ITrigger CreateCronTrigger(SysSchedule sysSchedule)
        {
            // 作业触发器
            return TriggerBuilder.Create()
                   .WithIdentity(sysSchedule.Name, sysSchedule.JobGroup)
                   .StartAt(sysSchedule.BeginTime.Value)//开始时间
                   .EndAt(sysSchedule.EndTime.Value)//结束数据
                   .WithCronSchedule(sysSchedule.Cron)//指定cron表达式
                   .ForJob(sysSchedule.Name, sysSchedule.JobGroup)//作业名称
                   .Build();
        }
        #endregion

    }
}

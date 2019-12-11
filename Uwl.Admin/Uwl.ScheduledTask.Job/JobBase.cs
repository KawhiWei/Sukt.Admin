using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Uwl.Common.LogsMethod;

namespace Uwl.ScheduledTask.Job
{
    public class JobBase
    {
        /// <summary>
        /// 执行指定任务
        /// </summary>
        /// <param name="context"></param>
        /// <param name="action"></param>
        public async Task ExecuteJob(IJobExecutionContext context, Func<Task> func)
        {
            try
            {
                var s = context.Trigger.Key.Name;
                //DbLogHelper.WriteRunInfo(task.TaskName + " 开始", task.TaskID.ToString(), "");
                //记录Job时间
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                await func();//执行任务
                // 3. 开始执行相关任务
                stopwatch.Stop();
                Console.Out.WriteLine("执行时间" + stopwatch.Elapsed.TotalMilliseconds);
                // 4. 记录Task 运行状态数据库
                //DbLogHelper.WriteRunInfo(task.TaskName + " 结束", task.TaskID.ToString(), "成功执行");
                LogServer.WriteLog(context.Trigger.Key.Name.Replace("-", ""), $"{context.Trigger.Key.Name}定时任务运行一切OK","任务结束");
            }
            catch (Exception ex)
            {
                JobExecutionException e2 = new JobExecutionException(ex);
                //true  是立即重新执行任务 
                e2.RefireImmediately = true;
                LogServer.WriteErrorLog(context.Trigger.Key.Name.Replace("-", ""), $"{context.Trigger.Key.Name}任务运行异常", ex);
                // 记录异常到数据库和 log 文件中。
                //DbLogHelper.WriteErrorInfo(ex);

            }
        }
    }
}

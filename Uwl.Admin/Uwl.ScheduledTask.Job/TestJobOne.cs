using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Quartz;

namespace Uwl.ScheduledTask.Job
{
    public class TestJobOne : IJob
    {
        public  async Task Execute(IJobExecutionContext context)
        {
            //记录Job时间
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            await Console.Out.WriteLineAsync(string.Format("测试任务1:任务分组：{0}，任务名称：{1}任务状态：{2}", context.JobDetail.Key.Group, context.JobDetail.Key.Name, "正常执行中"));
            stopwatch.Stop();
            if (stopwatch.Elapsed.TotalMilliseconds > 0)
            {
                //写入日志性能监控表和执行是否出错
            }
        }
    }
}

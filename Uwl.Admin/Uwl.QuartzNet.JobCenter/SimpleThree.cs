using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Uwl.QuartzNet.JobCenter
{
    public class SimpleThree : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync(string.Format("试一试:任务分组：{0}任务名称：{1}任务状态：{2}", context.JobDetail.Key.Group, context.JobDetail.Key.Name, "正常执行中"));
        }
    }
}

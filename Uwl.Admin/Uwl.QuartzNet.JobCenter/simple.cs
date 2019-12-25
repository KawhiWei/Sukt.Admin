using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Uwl.Common.Cache.RedisCache;

namespace Uwl.QuartzNet.JobCenter
{
    public class Simple : IJob
    {
        //private readonly IRedisCacheManager _redisCacheManager;
        //public Simple(IRedisCacheManager redisCacheManager)
        //{
        //    this._redisCacheManager = redisCacheManager;
        //}
        public async Task Execute(IJobExecutionContext context)
        {
            //记录Job时间
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            await Console.Out.WriteLineAsync(string.Format("试一试:任务分组：{0}任务名称：{1}任务状态：{2}", context.JobDetail.Key.Group, context.JobDetail.Key.Name, "正常执行中"));




            //var msg=await this._redisCacheManager.SubscribeRedis("testmes");

            //await Console.Out.WriteLineAsync("接收到消息"+msg);



            stopwatch.Stop();

            if (stopwatch.Elapsed.TotalMilliseconds > 0)
            {
                //写入日志性能监控表和执行是否出错
            }
        }
    }
}

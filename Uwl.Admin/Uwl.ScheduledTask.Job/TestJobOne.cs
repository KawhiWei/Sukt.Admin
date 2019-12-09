using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Uwl.Common.Cache.RedisCache;
using Uwl.Common.Subscription;
using Uwl.Data.Server.MenuServices;

namespace Uwl.ScheduledTask.Job
{
    public class TestJobOne : IJob
    {
        private readonly IRedisCacheManager _redisCacheManager;
        private readonly IMenuServer _menuServer;
        public TestJobOne(IRedisCacheManager redisCacheManager,IMenuServer menuServer)
        {
            this._redisCacheManager = redisCacheManager;
            this._menuServer = menuServer;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            //记录Job时间
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            
            await Console.Out.WriteLineAsync("我是有Redis的注入测试任务");
            var list = await _menuServer.GetMenuList();
            await Console.Out.WriteLineAsync("菜单表里总数量" + list.Count.ToString());
            stopwatch.Stop();
            await Console.Out.WriteLineAsync("执行时间" +  stopwatch.Elapsed.TotalMilliseconds);
            //if (stopwatch.Elapsed.TotalMilliseconds > 0)
            //{
            //    //写入日志性能监控表和执行是否出错
            //}
        }
    }
}

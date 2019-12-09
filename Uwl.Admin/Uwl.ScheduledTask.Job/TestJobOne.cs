using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Uwl.Common.Cache.RedisCache;
using Uwl.Common.Subscription;
using Uwl.Data.Server.MenuServices;

namespace Uwl.ScheduledTask.Job
{
    public class TestJobOne : JobBase,IJob
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
            await ExecuteJob(context, Run);
        }
        public async Task  Run()
        {
            await Console.Out.WriteLineAsync("我是有Redis的注入测试任务");
            var list = await _menuServer.GetMenuList();
            await Console.Out.WriteLineAsync("菜单表里总数量" + list.Count.ToString());
        }
    }
}

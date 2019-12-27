using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Uwl.Common.Cache.RedisCache;
using Uwl.Common.RabbitMQ;
using Uwl.Common.Subscription;
using Uwl.Data.Server.MenuServices;

namespace Uwl.ScheduledTask.Job
{
    public class TestJobOne : JobBase,IJob
    {
        private readonly IRedisCacheManager _redisCacheManager;
        private readonly IMenuServer _menuServer;
        private readonly IRabbitMQ _rabbitMQ;
        private readonly IRedisSubscription _redisSubscription;
        public TestJobOne(IRedisCacheManager redisCacheManager,IMenuServer menuServer, IRabbitMQ rabbitMQ, IRedisSubscription redisSubscription)
        {
            this._redisCacheManager = redisCacheManager;
            this._menuServer = menuServer;
            this._rabbitMQ = rabbitMQ;
            this._redisSubscription = redisSubscription;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            await ExecuteJob(context, Run);
        }
        public async Task  Run()
        {
            try
            {
                var list = await _menuServer.GetMenuList();
                this._rabbitMQ.SendData("Job1", "菜单表里总数量" + list.Count.ToString());
                await this._redisSubscription.PublishAsyncRedis("redis","我是redis的消息队列");
                await Console.Out.WriteLineAsync("菜单表里总数量" + list.Count.ToString());
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }
    }
}

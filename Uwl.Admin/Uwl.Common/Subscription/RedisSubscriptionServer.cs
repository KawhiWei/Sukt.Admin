using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Uwl.Common.Helper;

namespace Uwl.Common.Subscription
{
    public class RedisSubscriptionServer : IRedisSubscription
    {
        private readonly string redisConnectionString;
        public volatile ConnectionMultiplexer redisConnection;
        private readonly object redisConnectionLock = new object();
        /// <summary>
        /// Redis管理器的构造函数
        /// </summary>
        public RedisSubscriptionServer()
        {
            var _redisConnection = Appsettings.app(new string[] { "RedisCaching", "ConnectionString" });//获取Redis链接字符串
            if (string.IsNullOrWhiteSpace(_redisConnection))
            {
                throw new ArgumentException("Redis链接字符串为空;请配置Redis链接服务器", nameof(_redisConnection));
            }
            this.redisConnectionString = _redisConnection;
            this.redisConnection = GetRedisConnection();///获取Redis链接实例服务
        }

        /// <summary>
        /// 核心代码，获取链接方式
        /// 通过双If 加 lock的方式实现单例模式
        /// </summary>
        /// <returns></returns>
        private ConnectionMultiplexer GetRedisConnection()
        {
            //如果已存在链接实例，直接返回
            if (this.redisConnection != null && this.redisConnection.IsConnected)
            {
                return this.redisConnection;
            }
            //加锁，防止异步编程中，出现单例无效的问题
            lock (redisConnectionLock)
            {
                if (this.redisConnection != null)
                {
                    this.redisConnection.Dispose();//如果存在实例链接释放Redis链接
                }
                try
                {
                    this.redisConnection = ConnectionMultiplexer.Connect(this.redisConnectionString);
                }
                catch (Exception)
                {
                    throw new Exception("Redis服务未启用，请开启该服务");
                }
            }
            return this.redisConnection;
        }

        public void DisposeCSRedis()
        {
            redisConnection.Dispose();
        }

        public async void PublishAsyncRedis(string ChannelName, string obj)
        {
            ISubscriber subcriber = redisConnection.GetSubscriber();
            await subcriber.PublishAsync(ChannelName, obj);
        }

        public async Task<string> SubscribeRedis(string ChannelName)
        {
            ISubscriber subcriber = redisConnection.GetSubscriber();
            string Msg = "";
            await subcriber.SubscribeAsync(ChannelName, (channel, message) =>
            {
                Msg = message;
            });
            return Msg;
        }
    }
}

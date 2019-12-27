using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Uwl.Common.Helper;
using Uwl.Extends.Utility;

namespace Uwl.Common.Cache.RedisCache
{
    public class RedisCacheManager: IRedisCacheManager
    {
        public volatile ConnectionMultiplexer redisConnection;
        private readonly object redisConnectionLock = new object();
        /// <summary>
        /// Redis管理器的构造函数
        /// </summary>
        public RedisCacheManager()
        {
            //var _redisConnection = Appsettings.app(new string[] { "RedisCaching", "ConnectionString" });//获取Redis链接字符串
            //if (string.IsNullOrWhiteSpace(_redisConnection))
            //{
            //    throw new ArgumentException("redis config is empty", nameof(_redisConnection));
            //}
            this.redisConnection = RedisConnectionHelp.Instance;///获取Redis链接实例服务
        }
        /// <summary>
        /// 核心代码，获取链接方式
        /// 通过双If 加 lock的方式实现单例模式
        /// </summary>
        /// <returns></returns>
        
        public async Task Clear()
        {
            foreach (var item in this.redisConnection.GetEndPoints())
            {
                var server = this.redisConnection.GetServer(item);
                foreach (var keys in server.Keys())
                {
                    await redisConnection.GetDatabase().KeyDeleteAsync(keys);
                }
            }
        }

        public async Task<TEntity> Get<TEntity>(string key)
        {
            var value = await redisConnection.GetDatabase().StringGetAsync(key);
            if (value.HasValue)
            {
                //需要用的反序列化，将Redis存储的Byte[]，进行反序列化
                return SerializeHelper.Deserialize<TEntity>(value);
            }
            else
            {
                return default(TEntity);
            }
        }
        public async Task<List<TEntity>> GetList<TEntity>(string key)
        {
            if (key.IsNullOrEmpty())
                throw new Exception("要获取缓存的Key不可为空");
            var value = await redisConnection.GetDatabase().StringGetAsync(key);
            if(value.HasValue)
            {
                var list=SerializeHelper.Deserialize<List<TEntity>>(value);
                return list;
            }
            else
            {
                return new List<TEntity>();
            }
        }

        public async Task<bool> Get(string key)
        {
            return await redisConnection.GetDatabase().KeyExistsAsync(key);
        }

        public async Task<string> GetValue(string key)
        {
            return await redisConnection.GetDatabase().StringGetAsync(key);
        }

        public async Task Remove(string key)
        {
            if(await Get(key))
                await redisConnection.GetDatabase().KeyDeleteAsync(key);
        }

        public async Task Set(string key, object value, int? cacheTime=null)
        {
            if (value != null)
            {
                if(cacheTime!=null)
                {
                    await redisConnection.GetDatabase().StringSetAsync(key, SerializeHelper.Serialize(value), TimeSpan.FromSeconds(cacheTime.Value));
                }
                else
                {
                    await redisConnection.GetDatabase().StringSetAsync(key, SerializeHelper.Serialize(value));
                }
                
            }
        }
        public void DisposeCSRedis()
        {
            redisConnection.Dispose();
        }

        public async Task PublishAsyncRedis(string ChannelName, string obj)
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

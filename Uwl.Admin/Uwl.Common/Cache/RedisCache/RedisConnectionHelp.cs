using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using Uwl.Common.Helper;

namespace Uwl.Common.Cache.RedisCache
{
    public static class RedisConnectionHelp
    {
        private static readonly string redisConnection = Appsettings.app(new string[] { "RedisCaching", "ConnectionString" });//获取Redis链接字符串
        private static readonly object redisConnectionLock = new object();//加锁

        private static ConnectionMultiplexer _instance;
        public static ConnectionMultiplexer Instance
        {
            get
            {
                if(_instance==null)
                {
                    lock (redisConnectionLock)
                    {
                        if(_instance==null || _instance.IsConnected)
                        {
                            _instance = GetRedisConnection();
                        }
                    }
                }
                return _instance;
            }
        }
        private static ConnectionMultiplexer GetRedisConnection()
        {
            var connect =  ConnectionMultiplexer.Connect(redisConnection);
            //注册如下事件
            //connect.ConnectionFailed += MuxerConnectionFailed;
            //connect.ConnectionRestored += MuxerConnectionRestored;
            //connect.ErrorMessage += MuxerErrorMessage;
            //connect.ConfigurationChanged += MuxerConfigurationChanged;
            //connect.HashSlotMoved += MuxerHashSlotMoved;
            //connect.InternalError += MuxerInternalError;
            return connect;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Uwl.Common.Subscription
{
    /// <summary>
    /// 基于Redis的消息订阅接口定义
    /// </summary>
    public interface IRedisSubscription
    {
        /// <summary>
        /// 发布消息到Redis的某个队列频道
        /// </summary>
        /// <param name="ChannelName"></param>
        /// <param name="ojb"></param>
        void PublishAsyncRedis(string ChannelName,string obj);
        /// <summary>
        /// 获取订阅队列的消息
        /// </summary>
        /// <param name="ChannelName">订阅队列名称</param>
        Task<string> SubscribeRedis(string ChannelName);
        /// <summary>
        /// 释放Redis链接
        /// </summary>
        void DisposeCSRedis();
        
    }
}

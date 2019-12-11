using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Uwl.Common.Cache.RedisCache
{
    public interface IRedisCacheManager
    {
        /// <summary>
        /// 获取 Reids 缓存值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetValue(string key);

        /// <summary>
        /// 获取值，并序列化
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        TEntity Get<TEntity>(string key);

        /// <summary>
        /// 写入缓存到Redis
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="cacheTime">保存格式毫秒</param>
        void Set(string key, object value, int? cacheTime = null);

        /// <summary>
        /// 判断一个key是否存在Redis
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Get(string key);

        /// <summary>
        /// 移除某一个缓存值
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);

        /// <summary>
        /// 清除所有的Redis缓存
        /// </summary>
        void Clear();
        /// <summary>
        /// 获取List
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        List<TEntity> GetList<TEntity>(string key);
        /// <summary>
        /// 发布消息到Redis的某个队列频道
        /// </summary>
        /// <param name="ChannelName"></param>
        /// <param name="ojb"></param>
        void PublishAsyncRedis(string ChannelName, string obj);
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

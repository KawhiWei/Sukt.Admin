using Sukt.Core.Shared.Extensions;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Sukt.Core.Redis
{
    internal class CacheHelper
    {
        private static string GetKey<TKey>(TKey key)
        {
            key.NotNull(nameof(key));
            return key.ToString();
        }

        /// <summary>
        /// 得到
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>返回得到</returns>
        public static TCacheData Get<TKey, TCacheData>(TKey key)
        {
            return RedisHelper.Get<TCacheData>(GetKey(key));
        }

        /// <summary>
        /// 得到或添加
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static TCacheData GetOrAdd<TKey, TCacheData>(
         TKey key,
         Func<TCacheData> func)
        {
            key.NotNull(nameof(key));

            func.NotNull(nameof(func));
            var value = Get<TKey, TCacheData>(key);

            if (!Equals(value, default(TCacheData)))
            {
                return value;
            }

            value = func();

            if (Equals(value, default(TCacheData)))
            {
                return default;
            }

            Set<TKey, TCacheData>(key, value);
            return value;
        }

        /// <summary>
        /// 异步得到缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="token">键</param>
        /// <returns></returns>
        async public static Task<TCacheData> GetAsync<TKey, TCacheData>(TKey key, CancellationToken token = default)
        {
            return await RedisHelper.GetAsync<TCacheData>(GetKey(key));
        }

        /// <summary>
        /// 得到或添加
        /// </summary>
        /// <param name="key"><键/param>
        /// <param name="func"></param>
        /// <param name="token"></param>
        /// <returns>返回得到或添加后的缓存数据</returns>
        async public static Task<TCacheData> GetOrAddAsync<TKey, TCacheData>(
           [NotNull] TKey key,
           Func<Task<TCacheData>> func,
           CancellationToken token = default
       )
        {
            func.NotNull(nameof(func));
            var value = await GetAsync<TKey, TCacheData>(key);

            if (!Equals(value, default(TCacheData)))
            {
                return value;
            }

            value = await func();

            if (Equals(value, default(TCacheData)))
            {
                return default;
            }

            await SetAsync(key, value);
            return value;
        }

        #region 设置

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void Set<TKey, TCacheData>(TKey key, TCacheData value)
        {
            value.NotNull(nameof(value));
            RedisHelper.Set(GetKey(key), value);
        }

        /// <summary>
        /// 异步设置缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="token"></param>
        /// <returns></returns>
        async public static Task SetAsync<TKey, TCacheData>(TKey key, TCacheData value, CancellationToken token = default)
        {
            value.NotNull(nameof(value));
            await RedisHelper.SetAsync(GetKey(key), value);
        }

        #endregion 设置

        #region 删除

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key">要删除的键</param>
        public static void Remove<TKey>(TKey key)
        {
            RedisHelper.Del(GetKey(key));
        }

        /// <summary>
        /// 异步删除缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        async public static Task RemoveAsync<TKey>(TKey key, CancellationToken token = default)
        {
            await RedisHelper.DelAsync(GetKey(key));
        }

        #endregion 删除
    }
}
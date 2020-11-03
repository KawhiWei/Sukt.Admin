using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Sukt.Core.Caching
{
    public interface ICache
    {
        /// <summary>
        /// 得到
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>返回得到</returns>
        TCacheData Get<TCacheData>(string key);

        /// <summary>
        /// 得到或添加
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        TCacheData GetOrAdd<TCacheData>(
        string key,
        Func<TCacheData> func);

        /// <summary>
        /// 异步得到缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="token">键</param>
        /// <returns></returns>
        Task<TCacheData> GetAsync<TCacheData>(string key, CancellationToken token = default);

        /// <summary>
        /// 得到或添加
        /// </summary>
        /// <param name="key"><键/param>
        /// <param name="func"></param>
        /// <param name="token"></param>
        /// <returns>返回得到或添加后的缓存数据</returns>
        Task<TCacheData> GetOrAddAsync<TCacheData>(
             [NotNull] string key,
             Func<Task<TCacheData>> func,
             CancellationToken token = default
         );

        #region 设置

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        void Set<TCacheData>(string key, TCacheData value);

        /// <summary>
        /// 异步设置缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task SetAsync<TCacheData>(string key, TCacheData value, CancellationToken token = default);

        #endregion 设置

        #region 删除

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key">要删除的键</param>
        void Remove(string key);

        /// <summary>
        /// 异步删除缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task RemoveAsync(string key, CancellationToken token = default);

        #endregion 删除
    }
}
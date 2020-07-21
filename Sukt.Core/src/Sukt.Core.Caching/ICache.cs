using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Sukt.Core.Caching
{
    /// <summary>
    /// 缓存接口
    /// </summary>
    /// <typeparam name="TCacheData">缓存数据</typeparam>
    public interface ICache<TCacheData> : ICache<string, TCacheData>
         where TCacheData : class
    {

    }
    /// <summary>
    /// 缓存接口
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TCacheData">缓存数据</typeparam>
    public interface ICache<TKey, TCacheData>
         where TCacheData : class
    {

        /// <summary>
        /// 得到
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>返回得到</returns>
        TCacheData Get(TKey key);

        /// <summary>
        /// 得到或添加
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="func"></param>
        /// <returns></returns>
        TCacheData GetOrAdd(
        TKey key,
        Func<TCacheData> func);

        /// <summary>
        /// 异步得到缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="token">键</param>
        /// <returns></returns>
        Task<TCacheData> GetAsync(TKey key, CancellationToken token = default);

        /// <summary>
        /// 得到或添加
        /// </summary>
        /// <param name="key"><键/param>
        /// <param name="func"></param>
        /// <param name="token"></param>
        /// <returns>返回得到或添加后的缓存数据</returns>
        Task<TCacheData> GetOrAddAsync(
             [NotNull] TKey key,
             Func<Task<TCacheData>> func,
             CancellationToken token = default
         );


        #region 设置

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        void Set(TKey key, TCacheData value);

        /// <summary>
        /// 异步设置缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task SetAsync(TKey key, TCacheData value, CancellationToken token = default);
        #endregion



        #region 删除

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key">要删除的键</param>
        void Remove(TKey key);

        /// <summary>
        /// 异步删除缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task RemoveAsync(TKey key, CancellationToken token = default);
        #endregion
    }
}

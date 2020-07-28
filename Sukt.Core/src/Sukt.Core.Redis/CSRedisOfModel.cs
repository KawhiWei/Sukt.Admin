using Sukt.Core.Caching;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sukt.Core.Redis
{
    public class CSRedisCache<TCacheData> : CSRedisCache<string, TCacheData>, ICache<TCacheData>
               where TCacheData : class
    {

    }
    public class CSRedisCache<TKey, TCacheData> :
        ICache<TKey, TCacheData>
         where TCacheData : class
    {


        public TCacheData Get(TKey key)
        {

            return CacheHelper.Get<TKey, TCacheData>(key);
        }

        public async Task<TCacheData> GetAsync(TKey key, CancellationToken token = default)
        {

            return await CacheHelper.GetAsync<TKey, TCacheData>(key);
        }

        public TCacheData GetOrAdd(TKey key, Func<TCacheData> func)
        {

            return CacheHelper.GetOrAdd(key, func);
        }

        public async Task<TCacheData> GetOrAddAsync([NotNull] TKey key, Func<Task<TCacheData>> func, CancellationToken token = default)
        {


            return await CacheHelper.GetOrAddAsync(key, func);
        }

        public void Remove(TKey key)
        {

            CacheHelper.Remove(key);
        }

        public async Task RemoveAsync(TKey key, CancellationToken token = default)
        {

            await CacheHelper.RemoveAsync(key);
        }

        public void Set(TKey key, TCacheData value)
        {
            CacheHelper.Set(key, value);
        }

        public async Task SetAsync(TKey key, TCacheData value, CancellationToken token = default)
        {
            await CacheHelper.SetAsync(key, value);
        }
    }
}

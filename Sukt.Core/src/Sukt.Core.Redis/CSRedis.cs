using Sukt.Core.Caching;
using Sukt.Core.Shared.Extensions;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Sukt.Core.Redis
{
    public class CSRedis<TCacheData> : CSRedis<string, TCacheData>, ICache<TCacheData>
                where TCacheData : class
    {

    }
    public class CSRedis<TKey, TCacheData> :
        ICache<TKey, TCacheData>
         where TCacheData : class
    {

        private string GetKey(TKey key)
        {
            key.NotNull(nameof(key));
            return key.ToString();
        }

        public TCacheData Get(TKey key)
        {

            return RedisHelper.Get<TCacheData>(GetKey(key));
        }

        public async Task<TCacheData> GetAsync(TKey key, CancellationToken token = default)
        {

            return await RedisHelper.GetAsync<TCacheData>(GetKey(key));
        }

        public TCacheData GetOrAdd(TKey key, Func<TCacheData> func)
        {

            key.NotNull(nameof(key));

            func.NotNull(nameof(func));
            var value = this.Get(key);


            if (!Equals(value, default(TCacheData)))
            {
                return value;
            }

            value = func();

            if (Equals(value, default(TCacheData)))
            {
                return default;
            }

            Set(key, value);
            return value;
        }

        public async Task<TCacheData> GetOrAddAsync([NotNull] TKey key, Func<Task<TCacheData>> func, CancellationToken token = default)
        {


            func.NotNull(nameof(func));
            var value = await this.GetAsync(key);


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

        public void Remove(TKey key)
        {

            RedisHelper.Del(this.GetKey(key));
        }

        public async Task RemoveAsync(TKey key, CancellationToken token = default)
        {

            await RedisHelper.DelAsync(this.GetKey(key));
        }

        public void Set(TKey key, TCacheData value)
        {
            value.NotNull(nameof(value));
            RedisHelper.Set(GetKey(key), value);
        }

        public async Task SetAsync(TKey key, TCacheData value, CancellationToken token = default)
        {
            value.NotNull(nameof(value));
            await RedisHelper.SetAsync(GetKey(key), value);
        }
    }
}

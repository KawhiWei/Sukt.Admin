using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.Shared.Attributes.Dependency;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Shared.SuktDependencyAppModule
{
    /// <summary>
    /// 在同一个生命周期内保存数据使用，//也可以使用缓存，生命周期完成释放（本生命周期内保存数据）
    /// 
    /// </summary>
    [Dependency(ServiceLifetime.Scoped, AddSelf = true)]
    public class DictionaryAccessor : ConcurrentDictionary<string, object>, IDisposable
    {
        public void Dispose()
        {
            this.Clear();
        }
    }
}

using System;
using System.Linq;

namespace Sukt.Core.Shared.SuktReflection
{
    /// <summary>
    /// 抽象类
    /// </summary>
    public abstract class FinderBase<TItem> : IFinder<TItem>
    {
        private readonly object _syncObj = new object();

        public TItem[] Find(Func<TItem, bool> predicate)
        {
            return this.FindAll().Where(predicate).ToArray();
        }

        public TItem[] FindAll()
        {
            lock (_syncObj)
            {
                return this.FindAllItems();
            }
        }

        protected abstract TItem[] FindAllItems();
    }
}
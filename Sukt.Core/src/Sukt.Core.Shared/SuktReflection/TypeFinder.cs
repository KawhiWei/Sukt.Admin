using System;
using System.Linq;

namespace Sukt.Core.Shared.SuktReflection
{
    public class TypeFinder : FinderBase<Type>, ITypeFinder
    {
        private readonly IAssemblyFinder _assemblyFinder = null;
        private readonly object _syncObj = new object();

        public TypeFinder(IAssemblyFinder assemblyFinder)
        {
            _assemblyFinder = assemblyFinder;
        }

        protected override Type[] FindAllItems()
        {
            //Projects each element of a sequence to an System.Collections.Generic.IEnumerable`1
            //and flattens the resulting sequences into one sequence.
            return _assemblyFinder.FindAll().SelectMany(x => x.GetTypes()).ToArray();//返回数组
        }
    }
}
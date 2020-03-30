using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Sukt.Core.Shared.SuktReflection
{
    public class AssemblyFinder : FinderBase<Assembly>, IAssemblyFinder
    {
        protected override Assembly[] FindAllItems()
        {
            return AssemblyHelper.FindAllItems();
        }
    }
}

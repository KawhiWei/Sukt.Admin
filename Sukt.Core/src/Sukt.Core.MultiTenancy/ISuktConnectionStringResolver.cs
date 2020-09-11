using JetBrains.Annotations;
using Sukt.Core.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.MultiTenancy
{
    public interface ISuktConnectionStringResolver: IScopedDependency
    {
        [NotNull]
        string Resolve();
    }
}

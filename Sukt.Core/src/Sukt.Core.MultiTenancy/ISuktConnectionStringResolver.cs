using JetBrains.Annotations;
using Sukt.Core.Shared;

namespace Sukt.Core.MultiTenancy
{
    public interface ISuktConnectionStringResolver : IScopedDependency
    {
        [NotNull]
        string Resolve();
    }
}
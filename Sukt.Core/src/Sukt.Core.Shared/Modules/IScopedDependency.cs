using Sukt.Core.Shared.Attributes.Dependency;

namespace Sukt.Core.Shared
{
    /// <summary>
    /// 实现此接口的类型将自动注册为<see cref="ServiceLifetime.Scoped"/>模式
    /// </summary>
    [IgnoreDependency]
    public interface IScopedDependency
    {
    }
}
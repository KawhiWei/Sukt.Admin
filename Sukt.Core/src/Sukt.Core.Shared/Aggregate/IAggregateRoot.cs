using System.ComponentModel;

namespace Sukt.Core.Shared
{
    /// <summary>
    /// 领域聚合根
    /// </summary>
    public interface IAggregateRoot
    {
    }
    /// <summary>
    /// 领域聚合根主键
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IAggregateRoot<out TKey> : IAggregateRoot
    {
        [Description("主键")]
        TKey Id { get; }
    }
}

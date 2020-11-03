using System.Threading;
using System.Threading.Tasks;

namespace Sukt.Core.Shared.Events
{
    /// <summary>
    /// 事件处理基类
    /// </summary>
    public abstract class EventHandlerBase<TEvent> : IEventHandlerBase<TEvent> where TEvent : class, IEventBase
    {
        public abstract Task Handle(TEvent notification, CancellationToken cancellationToken);
    }
}
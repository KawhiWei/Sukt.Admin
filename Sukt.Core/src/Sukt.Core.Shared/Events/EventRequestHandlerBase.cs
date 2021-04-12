using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sukt.Core.Shared.Events
{
    /// <summary>
    /// 有返回值的事件处理抽象基类
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    /// <typeparam name="OutT"></typeparam>
    public abstract class EventRequestHandlerBase<TEvent,OutT> : IEventRequestHandlerBase<TEvent, OutT> where TEvent : class, IEventBase
    {
        public abstract Task<OutT> Handle(TEvent notification, CancellationToken cancellationToken);
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Shared.Events
{
    /// <summary>
    /// 有返回值的事件基类
    /// </summary>
    /// <typeparam name="OutT"></typeparam>
    public class RequestEventBase<OutT> : IEventBase, IRequest<OutT>
    {
        protected RequestEventBase()
        {
            EventAt = DateTimeOffset.UtcNow;
            EventId = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// 事件构造函数
        /// </summary>
        /// <param name="eventId"></param>
        protected RequestEventBase(string eventId)
        {
            EventId = eventId;
            EventAt = DateTimeOffset.UtcNow;
        }

        public RequestEventBase(string eventId, DateTimeOffset eventAt)
        {
            EventId = eventId;
            EventAt = eventAt;
        }

        /// <summary>
        /// 基类属性
        /// </summary>
        public DateTimeOffset EventAt { get; private set; }

        /// <summary>
        /// 基类属性
        /// </summary>
        public string EventId { get; private set; }
    }
}

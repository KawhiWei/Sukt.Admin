using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Shared.Events
{
    /// <summary>
    /// 事件处理基类
    /// </summary>
    public class EventBase : IEventBase, INotification
    {
        protected EventBase()
        {
            EventAt = DateTimeOffset.UtcNow;
            EventId = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 事件构造函数
        /// </summary>
        /// <param name="eventId"></param>
        protected EventBase(string eventId)
        {
            EventId = eventId;
            EventAt = DateTimeOffset.UtcNow;
        }
        public EventBase(string eventId, DateTimeOffset eventAt)
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

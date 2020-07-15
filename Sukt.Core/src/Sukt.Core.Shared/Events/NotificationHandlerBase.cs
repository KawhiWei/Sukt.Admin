using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Shared.Events
{
    public abstract class NotificationHandlerBase<TEvent> : EventHandlerBase<TEvent>, INotificationHandler<TEvent> where TEvent : EventBase
    {
    }
}

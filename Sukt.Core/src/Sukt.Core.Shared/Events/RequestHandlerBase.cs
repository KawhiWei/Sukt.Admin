using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Shared.Events
{
    /// <summary>
    /// 有返回值的事件处理基类
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    /// <typeparam name="OutT"></typeparam>
    public abstract class RequestHandlerBase<TEvent, OutT> : EventRequestHandlerBase<TEvent,OutT>, IRequestHandler<TEvent,OutT> where TEvent : RequestEventBase<OutT>
    {

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sukt.Core.Shared.Events
{
    /// <summary>
    /// 有返回值的事件处理接口
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    /// <typeparam name="OutT"></typeparam>
    public interface IEventRequestHandlerBase<in TEvent, OutT> where TEvent : class, IEventBase
    {
        /// <summary>
        /// 有返回值的事件处理
        /// </summary>
        /// <typeparam name="OutT"></typeparam>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<OutT> Handle(TEvent notification, CancellationToken cancellationToken);
    }
}

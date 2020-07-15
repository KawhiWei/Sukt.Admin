using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sukt.Core.Shared.Events.EventBus
{
    public interface IMediatorHandler
    {
        /// <summary>
        ///  发布事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="event"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : EventBase;
    }
}

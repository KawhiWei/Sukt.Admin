using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Shared.Events
{
    /// <summary>
    /// 消息队列事件总线
    /// </summary>
    public class EventQueue
    {
        private readonly ConcurrentDictionary<string, ConcurrentQueue<EventBase>> _eventQueues =
        new ConcurrentDictionary<string, ConcurrentQueue<EventBase>>();
        /// <summary>
        /// 排队一个队列
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="queueName"></param>
        /// <param name="event"></param>
        public void Enqueue<TEvent>(string queueName, TEvent @event) where TEvent : EventBase
        {
            var queue = _eventQueues.GetOrAdd(queueName, q => new ConcurrentQueue<EventBase>());
            queue.Enqueue(@event);
        }
        /// <summary>
        /// 尝试取消排队
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="event"></param>
        /// <returns></returns>
        public bool TryDequeue(string queueName, out EventBase @event)
        {
            var queue = _eventQueues.GetOrAdd(queueName, q => new ConcurrentQueue<EventBase>());
            return queue.TryDequeue(out @event);
        }
        /// <summary>
        /// 尝试删除一个队列
        /// </summary>
        /// <param name="queueName"></param>
        /// <returns></returns>
        public bool TryRemoveQueue(string queueName)
        {
            return _eventQueues.TryRemove(queueName, out _);
        }
        /// <summary>
        /// 是否包含队列
        /// </summary>
        /// <param name="queueName"></param>
        /// <returns></returns>
        public bool ContainsQueue(string queueName) => _eventQueues.ContainsKey(queueName);

        public ConcurrentQueue<EventBase> this[string queueName] => _eventQueues[queueName];
    }
}

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Sukt.Core.Shared.Events.EventBus;

namespace Sukt.Core.Shared.Extensions
{
    public static class EventBusExtensions
    {
        public static IServiceCollection AddEvents(this IServiceCollection services)
        {
            services.TryAddTransient<IEventBus, InMemoryDefaultBus>();//事件总线需要使用瞬时注入，否则在过滤器内无法获取当前字典
            return services;
        }
    }
}
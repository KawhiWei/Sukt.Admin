using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Sukt.Core.Shared.Events.EventBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sukt.Core.Shared.Extensions
{
    public static class EventBusExtensions
    {
        public static IServiceCollection AddEvents(this IServiceCollection services)
        {
            services.TryAddSingleton<IEventBus, InMemoryDefaultBus>();
            return services;
        }
    }
}

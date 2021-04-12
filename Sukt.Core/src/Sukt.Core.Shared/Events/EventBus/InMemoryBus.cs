using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Sukt.Core.Shared.Events.EventBus
{
    public sealed class InMemoryDefaultBus : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public InMemoryDefaultBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : IEventBase
        {
            return _mediator.Publish(@event);
        }

        public Task<OutT> SendAsync<OutT>(IRequest<OutT> @event, CancellationToken cancellationToken = default)
        {
            return _mediator.Send(@event);
        }
    }
}

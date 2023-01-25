using Warehouse.ProductService.Application.Contracts.Events;

namespace Warehouse.ProductService.Application.Contracts.Handlers
{
    public interface IIntegrationEventHandler<TEvent> where TEvent : IIntegrationEvent
    {
        public Task Process(TEvent @event, CancellationToken cancellationToken = default(CancellationToken));
    }
}

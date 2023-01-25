using Warehouse.OrderService.Application.Contracts.Events;

namespace WareHouse.OrderService.Application.Contracts.Handlers
{
    public interface IIntegrationEventHandler<TEvent> where TEvent : IIntegrationEvent
    {
        public Task Process(TEvent @event, CancellationToken cancellationToken = default(CancellationToken));
    }
}

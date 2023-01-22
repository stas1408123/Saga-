using Warehouse.Contracts.DTOs;
using WareHouse.IntegrationEvents;

namespace WareHouse.OrderService.Application.IntegrationEvents.Events
{
    public record OrderFinishedIntegrationEvent : IntegrationEvent<OrderDTO>
    {
        public OrderFinishedIntegrationEvent(OrderDTO Payload) : base(Payload) { }
    }
}

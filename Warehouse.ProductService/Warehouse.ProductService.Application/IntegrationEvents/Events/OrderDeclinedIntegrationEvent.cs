using Warehouse.Contracts.DTOs;
using WareHouse.IntegrationEvents;

namespace WareHouse.OrderService.Application.IntegrationEvents.Events
{
    public record OrderDeclinedIntegrationEvent : IntegrationEvent<OrderDTO>
    {
        public OrderDeclinedIntegrationEvent(OrderDTO Payload) : base(Payload) { }
    }
}

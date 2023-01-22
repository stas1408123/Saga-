using Warehouse.Contracts.DTOs;

namespace WareHouse.IntegrationEvents
{
    public record OrderDeclinedIntegrationEvent : IntegrationEvent<OrderDTO>
    {
        public OrderDeclinedIntegrationEvent(OrderDTO Payload) : base(Payload) { }
    }
}

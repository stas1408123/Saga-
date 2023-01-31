using Warehouse.Contracts.DTOs;

namespace WareHouse.IntegrationEvents
{
    public record OrderApprovedIntegrationEvent : IntegrationEvent<OrderDTO>
    {
        public OrderApprovedIntegrationEvent(OrderDTO Payload) : base(Payload) { }
    }
}

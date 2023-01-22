using Warehouse.Contracts.DTOs;

namespace WareHouse.IntegrationEvents
{
    public record OrderFinishedIntegrationEvent : IntegrationEvent<OrderDTO>
    {
        public OrderFinishedIntegrationEvent(OrderDTO Payload) : base(Payload) { }
    }
}

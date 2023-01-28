using Warehouse.Contracts.DTOs;

namespace WareHouse.IntegrationEvents
{
    public record InvalidOrderDetailsIntegrationEvent : IntegrationEvent<OrderDTO>
    {
        public InvalidOrderDetailsIntegrationEvent(OrderDTO Payload) : base(Payload)
        {
        }
    }
}

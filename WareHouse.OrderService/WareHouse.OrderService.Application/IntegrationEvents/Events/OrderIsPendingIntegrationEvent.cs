using Warehouse.Contracts.DTOs;

namespace WareHouse.IntegrationEvents
{
    public record OrderIsPendingIntegrationEvent : IntegrationEvent<OrderDTO>
    {
        public OrderIsPendingIntegrationEvent(OrderDTO Payload) : base(Payload)
        {
        }
    }
}

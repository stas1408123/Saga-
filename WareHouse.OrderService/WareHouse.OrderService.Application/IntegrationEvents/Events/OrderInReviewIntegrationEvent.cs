using Warehouse.Contracts.DTOs;

namespace WareHouse.IntegrationEvents
{
    public record OrderInReviewIntegrationEvent : IntegrationEvent<OrderDTO>
    {
        public OrderInReviewIntegrationEvent(OrderDTO Payload) : base(Payload)
        {
        }
    }
}
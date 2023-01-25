using Warehouse.Contracts.DTOs;
using WareHouse.IntegrationEvents;

namespace Warehouse.ProductService.Application.IntegrationEvents.Events
{
    public record OrderInReviewIntegrationEvent : IntegrationEvent<OrderDTO>
    {
        public OrderInReviewIntegrationEvent(OrderDTO Payload) : base(Payload)
        {
        }
    }
}

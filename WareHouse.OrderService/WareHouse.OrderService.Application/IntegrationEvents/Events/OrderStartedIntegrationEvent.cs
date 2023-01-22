using Warehouse.Contracts.DTOs;

namespace WareHouse.IntegrationEvents
{
    public record OrderStartedIntegrationEvent : IntegrationEvent<OrderDTO>
    {
        public OrderStartedIntegrationEvent(OrderDTO Payload) : base(Payload)
        {
        }
    }
}

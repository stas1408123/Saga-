using Warehouse.Contracts.DTOs;

namespace WareHouse.IntegrationEvents
{
    public record ProductOutOfStockIntegrationEvent : IntegrationEvent<ProductDTO>
    {
        public ProductOutOfStockIntegrationEvent(ProductDTO Payload) : base(Payload)
        {
        }
    }
}

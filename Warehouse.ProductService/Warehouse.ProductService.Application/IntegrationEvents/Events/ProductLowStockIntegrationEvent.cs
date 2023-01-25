using Warehouse.Contracts.DTOs;

namespace WareHouse.IntegrationEvents
{
    public record ProductLowStockIntegrationEvent : IntegrationEvent<ProductDTO>
    {
        public ProductLowStockIntegrationEvent(ProductDTO Payload) : base(Payload) { }
    }
}

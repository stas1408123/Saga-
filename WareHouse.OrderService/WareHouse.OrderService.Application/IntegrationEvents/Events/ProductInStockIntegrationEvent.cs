using Warehouse.Contracts.DTOs;

namespace WareHouse.IntegrationEvents
{
    public record ProductInStockIntegrationEvent : IntegrationEvent<ProductDTO>
    {
        public ProductInStockIntegrationEvent(ProductDTO Payload) : base(Payload)
        {
        }
    }
}

using Warehouse.Contracts.DTOs;

namespace WareHouse.IntegrationEvents
{
    public record ProductInStockIntegrationEvent : IntegrationEvent<ProductDTO>
    {
        public string? OrderId { get; set; }
        public ProductInStockIntegrationEvent(ProductDTO Payload) : base(Payload) { }
    }
}

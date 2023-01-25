using Warehouse.Contracts.DTOs.Enums;

namespace Warehouse.Contracts.DTOs
{
    public record ProductDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public StockStatus StockStatus { get; set; }

        public int CategoryId { get; set; }
        public string? OrderId { get; set; }

        public ProductDTO() { }
    }
}

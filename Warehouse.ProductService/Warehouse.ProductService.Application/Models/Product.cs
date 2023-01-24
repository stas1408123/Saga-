using WarehouseService.Domain.Enums;

namespace Warehouse.ProductService.Application.Models
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public int Quantity { get; set; }
        public StockStatus StockStatus { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}

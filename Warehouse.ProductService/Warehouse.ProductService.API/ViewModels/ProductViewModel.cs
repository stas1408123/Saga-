using WarehouseService.Domain.Enums;

namespace Warehouse.ProductService.API.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public int Quantity { get; set; }
        public StockStatus StockStatus { get; set; }

        public int CategoryId { get; set; }
        public CategoryViewModel? Category { get; set; }
    }
}

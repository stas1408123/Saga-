using WarehouseService.Domain.Enums;

namespace Warehouse.ProductService.API.ViewModels
{
    public class UpdateProductViewModel
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public int Quantity { get; set; }
        public StockStatus StockStatus { get; set; } = StockStatus.InStock;
        public int CategoryId { get; set; }
    }
}

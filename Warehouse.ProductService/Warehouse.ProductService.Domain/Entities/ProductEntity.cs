using WarehouseService.Domain.Entities.Base;
using WarehouseService.Domain.Enums;

namespace WarehouseService.Domain.Entities
{
    public class ProductEntity : EntityBase
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public int Quantity { get; set; }
        public StockStatus StockStatus { get; set; }

        public int CategoryId { get; set; }
        public CategoryEntity? Category { get; set; }
    }
}

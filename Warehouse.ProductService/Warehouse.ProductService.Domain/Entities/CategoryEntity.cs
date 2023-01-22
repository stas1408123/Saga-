using WarehouseService.Domain.Entities.Base;

namespace WarehouseService.Domain.Entities
{
    public class CategoryEntity : EntityBase
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public int LowStock { get; set; }
        public int OutOfStock { get; set; }

        ICollection<ProductEntity>? Products = new List<ProductEntity>();
    }
}

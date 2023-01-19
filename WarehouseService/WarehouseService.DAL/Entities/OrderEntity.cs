using WarehouseService.Domain.Entities.Base;
using WarehouseService.Domain.Enums;

namespace WarehouseService.Domain.Entities
{
    public class OrderEntity : EntityBase
    {
        public int ProductAmount { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateOnly? Date { get; set; }

        public int ProductId { get; set; }
        public ProductEntity? Product { get; set; }
    }
}

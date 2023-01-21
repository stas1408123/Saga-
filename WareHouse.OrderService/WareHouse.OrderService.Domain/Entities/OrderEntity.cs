using WareHouse.OrderService.Domain.Attributes;
using WareHouse.OrderService.Domain.Entities.Base;
using WareHouse.OrderService.Domain.Enums;

namespace WareHouse.OrderService.Domain.Entities
{
    [BsonCollectionAttribute("orders")]
    public class OrderEntity : EntityBase
    {
        public int ProductAmount { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateOnly? Date { get; set; }

        public int ProductId { get; set; }
    }
}

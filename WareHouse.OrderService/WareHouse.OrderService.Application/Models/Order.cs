using WareHouse.OrderService.Domain.Enums;

namespace WareHouse.OrderService.Application.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int ProductAmount { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateOnly? Date { get; set; }

        public int ProductId { get; set; }
    }
}

using WareHouse.OrderService.Domain.Enums;

namespace WareHouse.OrderService.API.ViewModels
{
    public class OrderViewModel
    {
        public required string Id { get; set; }
        public int ProductAmount { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime? Date { get; set; }
        public int ProductId { get; set; }
    }
}

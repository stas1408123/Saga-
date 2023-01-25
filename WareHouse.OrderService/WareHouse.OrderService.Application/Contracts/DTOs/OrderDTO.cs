using Warehouse.Contracts.DTOs.Enums;

namespace Warehouse.Contracts.DTOs
{
    public record OrderDTO
    {
        public required string Id { get; set; }
        public int ProductAmount { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime Date { get; set; }

        public int ProductId { get; set; }

        public OrderDTO() { }
    };
}

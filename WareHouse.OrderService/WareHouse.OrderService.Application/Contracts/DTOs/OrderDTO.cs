using Warehouse.Contracts.DTOs.Enums;

namespace Warehouse.Contracts.DTOs
{
    public record OrderDTO(string Id, int ProductAmount, OrderStatus OrderStatus, DateTime Date, int ProductId);
}

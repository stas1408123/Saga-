using Warehouse.Contracts.DTOs.Enums;

namespace Warehouse.Contracts.DTOs
{
    public record ProductDTO(int Id, string Name, string Description, int Quantity, StockStatus StockStatus, int CategoryId, string OrderId);
}

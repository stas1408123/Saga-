namespace Warehouse.Contracts.DTOs
{
    public record FaultDTO(ProductDTO? Product, OrderDTO? Order, Type eventType);
}

using Warehouse.ProductService.Application.Contracts.Events;

namespace Warehouse.Contracts.DTOs
{
    public record FaultDTO(IIntegrationEvent @event, Type eventType);
}

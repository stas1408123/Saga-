using Warehouse.ProductService.Application.Contracts.Events;

namespace WareHouse.IntegrationEvents
{
    public record IntegrationEvent<TPayload>(TPayload Payload) : IIntegrationEvent where TPayload : class;
}

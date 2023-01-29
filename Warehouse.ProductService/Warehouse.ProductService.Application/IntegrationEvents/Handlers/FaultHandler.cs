using Microsoft.Extensions.Logging;
using Warehouse.ProductService.Application.Contracts.Handlers;
using Warehouse.ProductService.Application.Contracts.Repositories;
using WareHouse.IntegrationEvents;

namespace Warehouse.ProductService.Application.IntegrationEvents.Handlers
{
    public class FaultHandler : IIntegrationEventHandler<FaultIntegrationEvent>
    {
        private IProductRepository _productRepository;
        private readonly ILogger<FaultHandler> _logger;

        public FaultHandler(IProductRepository productRepository, ILogger<FaultHandler> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public Task Process(FaultIntegrationEvent @event, CancellationToken cancellationToken = default)
        {
            var order = @event.Payload.Order;

            _logger.LogWarning("Order: {id} for product {productId}. Something faulted. Reverting changes...", order.Id, order.ProductId);

            _productRepository.RevertChanges();

            return Task.CompletedTask;
        }
    }
}

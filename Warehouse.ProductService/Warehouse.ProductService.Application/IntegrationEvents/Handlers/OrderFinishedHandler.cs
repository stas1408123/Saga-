using Microsoft.Extensions.Logging;
using Warehouse.ProductService.Application.Contracts.Handlers;
using Warehouse.ProductService.Application.Contracts.Repositories;
using WareHouse.IntegrationEvents;

namespace Warehouse.ProductService.Application.IntegrationEvents.Handlers
{
    public class OrderFinishedHandler : IIntegrationEventHandler<OrderFinishedIntegrationEvent>
    {
        private readonly ILogger<OrderFinishedHandler> _logger;
        private readonly IProductRepository _productRepository;

        public OrderFinishedHandler(ILogger<OrderFinishedHandler> logger, IProductRepository productRepository)
        {
            ArgumentNullException.ThrowIfNull(logger);

            _logger = logger;
            _productRepository = productRepository;
        }

        public Task Process(OrderFinishedIntegrationEvent @event, CancellationToken cancellationToken = default)
        {
            var order = @event.Payload;

            _logger.LogInformation("Order finished event. Order id: {id} for product {productId} successfully completed.", order.Id, order.ProductId);

            return Task.CompletedTask;
        }
    }
}

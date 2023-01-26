using Microsoft.Extensions.Logging;
using Warehouse.ProductService.Application.Contracts.Handlers;
using WareHouse.IntegrationEvents;

namespace Warehouse.ProductService.Application.IntegrationEvents.Handlers
{
    public class OrderFinishedHandler : IIntegrationEventHandler<OrderFinishedIntegrationEvent>
    {
        private readonly ILogger<OrderFinishedHandler> _logger;

        public OrderFinishedHandler(ILogger<OrderFinishedHandler> logger)
        {
            ArgumentNullException.ThrowIfNull(logger);

            _logger = logger;
        }

        public Task Process(OrderFinishedIntegrationEvent @event, CancellationToken cancellationToken = default)
        {
            var order = @event.Payload;
            _logger.LogInformation("Order finished event. Order id: {id} for product {productId} successfully completed.", order.Id, order.ProductId);

            return Task.CompletedTask;
        }
    }
}

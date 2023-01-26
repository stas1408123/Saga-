using MassTransit;
using Microsoft.Extensions.Logging;
using Warehouse.ProductService.Application.Contracts.Handlers;
using WareHouse.IntegrationEvents;

namespace Warehouse.ProductService.Application.IntegrationEvents.Handlers
{

    public class OrderApprovedHandler : IIntegrationEventHandler<OrderApprovedIntegrationEvent>
    {
        private readonly ILogger<OrderApprovedHandler> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public OrderApprovedHandler(ILogger<OrderApprovedHandler> logger, IPublishEndpoint publishEndpoint)
        {
            ArgumentNullException.ThrowIfNull(logger);
            ArgumentNullException.ThrowIfNull(publishEndpoint);

            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Process(OrderApprovedIntegrationEvent @event, CancellationToken cancellationToken = default)
        {
            var order = @event.Payload;
            _logger.LogInformation("Order approved event. Order id: {id} for product {productId}. Start finishing order", order.Id, order.ProductId);

            await _publishEndpoint.Publish(new OrderFinishedIntegrationEvent(order), cancellationToken);
        }
    }
}

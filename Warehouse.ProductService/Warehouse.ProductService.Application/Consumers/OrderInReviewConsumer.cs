using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Warehouse.ProductService.Application.Contracts.Handlers;
using WareHouse.IntegrationEvents;

namespace Warehouse.ProductService.Application.Consumers
{
    public class OrderInReviewConsumer : IConsumer<OrderInReviewIntegrationEvent>
    {
        private readonly ILogger<OrderInReviewConsumer> _logger;
        private readonly IIntegrationEventHandler<OrderInReviewIntegrationEvent> _eventHandler;

        public OrderInReviewConsumer(ILogger<OrderInReviewConsumer> logger, IIntegrationEventHandler<OrderInReviewIntegrationEvent> eventHandler)
        {
            ArgumentNullException.ThrowIfNull(logger);
            ArgumentNullException.ThrowIfNull(eventHandler);

            _logger = logger;
            _eventHandler = eventHandler;
        }

        public async Task Consume(ConsumeContext<OrderInReviewIntegrationEvent> context)
        {
            var jsonMessage = JsonConvert.SerializeObject(context.Message);
            _logger.LogInformation("Order in review. Order: {order}", jsonMessage);

            await _eventHandler.Process(context.Message);
        }
    }
}

using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Warehouse.ProductService.Application.Contracts.Handlers;
using WareHouse.IntegrationEvents;

namespace Warehouse.ProductService.Application.Consumers
{
    public class OrderApprovedConsumer : IConsumer<OrderApprovedIntegrationEvent>
    {
        private readonly ILogger<OrderApprovedConsumer> _logger;
        private readonly IIntegrationEventHandler<OrderApprovedIntegrationEvent> _eventHandler;

        public OrderApprovedConsumer(ILogger<OrderApprovedConsumer> logger, IIntegrationEventHandler<OrderApprovedIntegrationEvent> eventHandler)
        {
            ArgumentNullException.ThrowIfNull(logger);
            ArgumentNullException.ThrowIfNull(eventHandler);

            _logger = logger;
            _eventHandler = eventHandler;
        }

        public async Task Consume(ConsumeContext<OrderApprovedIntegrationEvent> context)
        {
            var jsonMessage = JsonConvert.SerializeObject(context.Message);
            _logger.LogInformation("Order approved. Order: {order}", jsonMessage);

            await _eventHandler.Process(context.Message);
        }
    }
}

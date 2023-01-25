using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Warehouse.ProductService.Application.Contracts.Handlers;
using WareHouse.IntegrationEvents;

namespace Warehouse.ProductService.Application.Consumers
{
    public class OrderStartedConsumer : IConsumer<OrderStartedIntegrationEvent>
    {
        private readonly ILogger<OrderStartedConsumer> _logger;
        private readonly IIntegrationEventHandler<OrderStartedIntegrationEvent> _eventHandler;

        public OrderStartedConsumer(ILogger<OrderStartedConsumer> logger, IIntegrationEventHandler<OrderStartedIntegrationEvent> eventHandler)
        {
            _logger = logger;
            _eventHandler = eventHandler;
        }

        public async Task Consume(ConsumeContext<OrderStartedIntegrationEvent> context)
        {
            var jsonMessage = JsonConvert.SerializeObject(context.Message);
            _logger.LogInformation("Order started. Order: {order}", jsonMessage);

            await _eventHandler.Process(context.Message);
        }
    }
}

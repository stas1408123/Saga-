using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WareHouse.IntegrationEvents;
using WareHouse.OrderService.Application.Contracts.Handlers;

namespace WareHouse.OrderService.Application.Consumers
{
    public class InvalidOrderDetailsConsumer : IConsumer<InvalidOrderDetailsIntegrationEvent>
    {
        private readonly ILogger<InvalidOrderDetailsConsumer> _logger;
        private readonly IIntegrationEventHandler<InvalidOrderDetailsIntegrationEvent> _eventHandler;

        public InvalidOrderDetailsConsumer(ILogger<InvalidOrderDetailsConsumer> logger, IIntegrationEventHandler<InvalidOrderDetailsIntegrationEvent> eventHandler)
        {
            _logger = logger;
            _eventHandler = eventHandler;
        }

        public async Task Consume(ConsumeContext<InvalidOrderDetailsIntegrationEvent> context)
        {
            var jsonMessage = JsonConvert.SerializeObject(context.Message);
            _logger.LogInformation("Received product in stock event. Product: {product}", jsonMessage);

            await _eventHandler.Process(context.Message);
        }
    }
}

using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WareHouse.IntegrationEvents;
using WareHouse.OrderService.Application.Contracts.Handlers;

namespace WareHouse.OrderService.Application.Consumers
{
    public class ProductOutOfStockConsumer : IConsumer<ProductOutOfStockIntegrationEvent>
    {
        private readonly ILogger<ProductOutOfStockConsumer> _logger;
        private readonly IIntegrationEventHandler<ProductOutOfStockIntegrationEvent> _eventHandler;

        public ProductOutOfStockConsumer(ILogger<ProductOutOfStockConsumer> logger, IIntegrationEventHandler<ProductOutOfStockIntegrationEvent> eventHandler)
        {
            _logger = logger;
            _eventHandler = eventHandler;
        }

        public async Task Consume(ConsumeContext<ProductOutOfStockIntegrationEvent> context)
        {
            var jsonMessage = JsonConvert.SerializeObject(context.Message);
            _logger.LogInformation("Received product out of stock event. Product: {product}", jsonMessage);

            await _eventHandler.Process(context.Message);
        }
    }
}

using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WareHouse.IntegrationEvents;
using WareHouse.OrderService.Application.Contracts.Handlers;

namespace WareHouse.OrderService.Application.Consumers
{
    public class ProductLowStockConsumer : IConsumer<ProductLowStockIntegrationEvent>
    {
        private readonly ILogger<ProductLowStockConsumer> _logger;
        private readonly IIntegrationEventHandler<ProductLowStockIntegrationEvent> _eventHandler;

        public ProductLowStockConsumer(ILogger<ProductLowStockConsumer> logger, IIntegrationEventHandler<ProductLowStockIntegrationEvent> eventHandler)
        {
            _logger = logger;
            _eventHandler = eventHandler;
        }

        public async Task Consume(ConsumeContext<ProductLowStockIntegrationEvent> context)
        {
            var jsonMessage = JsonConvert.SerializeObject(context.Message);
            _logger.LogInformation("Received product in low stock event. Product: {product}", jsonMessage);

            await _eventHandler.Process(context.Message);
        }
    }
}

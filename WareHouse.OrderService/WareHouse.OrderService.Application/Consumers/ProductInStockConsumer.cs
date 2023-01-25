using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WareHouse.IntegrationEvents;
using WareHouse.OrderService.Application.Contracts.Handlers;

namespace WareHouse.OrderService.Application.Consumers
{
    public class ProductInStockConsumer : IConsumer<ProductInStockIntegrationEvent>
    {
        private readonly ILogger<ProductInStockConsumer> _logger;
        private readonly IIntegrationEventHandler<ProductInStockIntegrationEvent> _eventHandler;

        public ProductInStockConsumer(ILogger<ProductInStockConsumer> logger, IIntegrationEventHandler<ProductInStockIntegrationEvent> eventHandler)
        {
            _logger = logger;
            _eventHandler = eventHandler;
        }

        public async Task Consume(ConsumeContext<ProductInStockIntegrationEvent> context)
        {
            var jsonMessage = JsonConvert.SerializeObject(context.Message);
            _logger.LogInformation("Received product in stock event. Product: {product}", jsonMessage);

            await _eventHandler.Process(context.Message);
        }
    }
}

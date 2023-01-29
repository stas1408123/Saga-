using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Warehouse.Contracts.DTOs;
using WareHouse.IntegrationEvents;
using WareHouse.OrderService.Application.Contracts.Handlers;

namespace WareHouse.OrderService.Application.Consumers
{
    public class ProductLowStockConsumer : IConsumer<ProductLowStockIntegrationEvent>
    {
        private readonly ILogger<ProductLowStockConsumer> _logger;
        private readonly IIntegrationEventHandler<ProductLowStockIntegrationEvent> _eventHandler;
        private readonly IPublishEndpoint _publishEndpoint;

        public ProductLowStockConsumer(ILogger<ProductLowStockConsumer> logger, IIntegrationEventHandler<ProductLowStockIntegrationEvent> eventHandler, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _eventHandler = eventHandler;
            _publishEndpoint = publishEndpoint;

            ArgumentNullException.ThrowIfNull(logger);
            ArgumentNullException.ThrowIfNull(publishEndpoint);
            ArgumentNullException.ThrowIfNull(eventHandler);
        }

        public async Task Consume(ConsumeContext<ProductLowStockIntegrationEvent> context)
        {
            try
            {
                var jsonMessage = JsonConvert.SerializeObject(context.Message);
                _logger.LogInformation("Received product in low stock event. Product: {product}", jsonMessage);

                await _eventHandler.Process(context.Message);
            }
            catch (Exception)
            {
                _logger.LogError("Finish order id: {id}. Failure in while processing order for product id: {productId}. Publising event.", context.Message.Payload.OrderId, context.Message.Payload.Id);

                var faultData = new FaultDTO(context.Message.Payload, null, typeof(ProductLowStockIntegrationEvent));

                await _publishEndpoint.Publish(new FaultIntegrationEvent(faultData));
            }
        }
    }
}

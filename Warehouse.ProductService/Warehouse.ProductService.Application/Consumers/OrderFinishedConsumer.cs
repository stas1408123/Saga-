using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Warehouse.Contracts.DTOs;
using Warehouse.ProductService.Application.Contracts.Handlers;
using WareHouse.IntegrationEvents;

namespace Warehouse.ProductService.Application.Consumers
{
    public class OrderFinishedConsumer : IConsumer<OrderFinishedIntegrationEvent>
    {
        private readonly ILogger<OrderFinishedConsumer> _logger;
        private readonly IIntegrationEventHandler<OrderFinishedIntegrationEvent> _eventHandler;
        private readonly IPublishEndpoint _publishEndpoint;

        public OrderFinishedConsumer(ILogger<OrderFinishedConsumer> logger, IIntegrationEventHandler<OrderFinishedIntegrationEvent> eventHandler, IPublishEndpoint publishEndpoint)
        {
            ArgumentNullException.ThrowIfNull(logger);
            ArgumentNullException.ThrowIfNull(eventHandler);
            ArgumentNullException.ThrowIfNull(publishEndpoint);

            _logger = logger;
            _eventHandler = eventHandler;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<OrderFinishedIntegrationEvent> context)
        {
            try
            {
                var jsonMessage = JsonConvert.SerializeObject(context.Message);
                _logger.LogInformation("Order processing finished. Order: {order}", jsonMessage);

                await _eventHandler.Process(context.Message);
            }
            catch
            {
                _logger.LogError("Finish order id: {id}. Failure in while processing product id: {productId}. Publising event.", context.Message.Payload.Id, context.Message.Payload.ProductId);

                var faultData = new FaultDTO(null, context.Message.Payload);

                await _publishEndpoint.Publish(new FaultIntegrationEvent(faultData));
            }
        }
    }
}

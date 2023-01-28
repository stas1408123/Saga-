using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Warehouse.Contracts.DTOs;
using Warehouse.ProductService.Application.Contracts.Handlers;
using WareHouse.IntegrationEvents;

namespace Warehouse.ProductService.Application.Consumers
{
    public class OrderDeclinedConsumer : IConsumer<OrderDeclinedIntegrationEvent>
    {
        private readonly ILogger<OrderDeclinedConsumer> _logger;
        private readonly IIntegrationEventHandler<OrderDeclinedIntegrationEvent> _eventHandler;
        private readonly IPublishEndpoint _publishEndpoint;

        public OrderDeclinedConsumer(ILogger<OrderDeclinedConsumer> logger, IIntegrationEventHandler<OrderDeclinedIntegrationEvent> eventHandler, IPublishEndpoint publishEndpoint)
        {
            ArgumentNullException.ThrowIfNull(logger);
            ArgumentNullException.ThrowIfNull(eventHandler);

            _logger = logger;
            _eventHandler = eventHandler;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<OrderDeclinedIntegrationEvent> context)
        {
            try
            {
                var jsonMessage = JsonConvert.SerializeObject(context.Message);
                _logger.LogInformation("Order declined. Order: {order}", jsonMessage);

                await _eventHandler.Process(context.Message);
            }
            catch
            {
                var faultData = new FaultDTO(context.Message, typeof(OrderDeclinedIntegrationEvent));

                await _publishEndpoint.Publish(new FaultIntegrationEvent(faultData));
            }
        }
    }
}

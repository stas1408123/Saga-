using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Warehouse.Contracts.DTOs;
using Warehouse.ProductService.Application.Contracts.Handlers;
using WareHouse.IntegrationEvents;

namespace Warehouse.ProductService.Application.Consumers
{
    public class OrderApprovedConsumer : IConsumer<OrderApprovedIntegrationEvent>
    {
        private readonly ILogger<OrderApprovedConsumer> _logger;
        private readonly IIntegrationEventHandler<OrderApprovedIntegrationEvent> _eventHandler;
        private readonly IPublishEndpoint _publishEndpoint;

        public OrderApprovedConsumer(ILogger<OrderApprovedConsumer> logger, IIntegrationEventHandler<OrderApprovedIntegrationEvent> eventHandler, IPublishEndpoint publishEndpoint)
        {
            ArgumentNullException.ThrowIfNull(logger);
            ArgumentNullException.ThrowIfNull(eventHandler);

            _logger = logger;
            _eventHandler = eventHandler;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<OrderApprovedIntegrationEvent> context)
        {
            try
            {
                var jsonMessage = JsonConvert.SerializeObject(context.Message);
                _logger.LogInformation("Order approved. Order: {order}", jsonMessage);

                await _eventHandler.Process(context.Message);
            }
            catch
            {
                var faultData = new FaultDTO(context.Message, typeof(OrderApprovedIntegrationEvent));

                await _publishEndpoint.Publish(new FaultIntegrationEvent(faultData));
            }
        }
    }
}

using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Warehouse.Contracts.DTOs;
using Warehouse.ProductService.Application.Contracts.Handlers;
using WareHouse.IntegrationEvents;

namespace Warehouse.ProductService.Application.Consumers
{
    public class OrderInReviewConsumer : IConsumer<OrderInReviewIntegrationEvent>
    {
        private readonly ILogger<OrderInReviewConsumer> _logger;
        private readonly IIntegrationEventHandler<OrderInReviewIntegrationEvent> _eventHandler;
        private readonly IPublishEndpoint _publishEndpoint;

        public OrderInReviewConsumer(ILogger<OrderInReviewConsumer> logger, IIntegrationEventHandler<OrderInReviewIntegrationEvent> eventHandler, IPublishEndpoint publishEndpoint)
        {
            ArgumentNullException.ThrowIfNull(logger);
            ArgumentNullException.ThrowIfNull(eventHandler);
            ArgumentNullException.ThrowIfNull(publishEndpoint);

            _logger = logger;
            _eventHandler = eventHandler;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<OrderInReviewIntegrationEvent> context)
        {
            try
            {
                var jsonMessage = JsonConvert.SerializeObject(context.Message);
                _logger.LogInformation("Order in review. Order: {order}", jsonMessage);

                await _eventHandler.Process(context.Message);
            }
            catch
            {
                _logger.LogError("Put in review order id: {id}. Failure in while processing product id: {productId}. Publising event.", context.Message.Payload.Id, context.Message.Payload.ProductId);

                var faultData = new FaultDTO(context.Message, typeof(OrderInReviewIntegrationEvent));

                await _publishEndpoint.Publish(new FaultIntegrationEvent(faultData));
            }
        }
    }
}

using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Warehouse.ProductService.Application.Contracts.Handlers;
using WareHouse.IntegrationEvents;

namespace Warehouse.ProductService.Application.Consumers
{
    public class OrderFinishedConsumer : IConsumer<OrderFinishedIntegrationEvent>
    {
        private readonly ILogger<OrderFinishedConsumer> _logger;
        private readonly IIntegrationEventHandler<OrderFinishedIntegrationEvent> _eventHandler;

        public OrderFinishedConsumer(ILogger<OrderFinishedConsumer> logger, IIntegrationEventHandler<OrderFinishedIntegrationEvent> eventHandler)
        {
            _logger = logger;
            _eventHandler = eventHandler;
        }

        public async Task Consume(ConsumeContext<OrderFinishedIntegrationEvent> context)
        {
            var jsonMessage = JsonConvert.SerializeObject(context.Message);
            _logger.LogInformation("Order finished. Order: {order}", jsonMessage);

            await _eventHandler.Process(context.Message);
        }
    }
}

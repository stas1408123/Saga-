using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Warehouse.ProductService.Application.Contracts.Handlers;
using WareHouse.IntegrationEvents;

namespace Warehouse.ProductService.Application.Consumers
{
    public class FaultConsumer : IConsumer<FaultIntegrationEvent>
    {
        private readonly ILogger<FaultConsumer> _logger;
        private readonly IIntegrationEventHandler<FaultIntegrationEvent> _eventHandler;

        public FaultConsumer(ILogger<FaultConsumer> logger, IIntegrationEventHandler<FaultIntegrationEvent> eventHandler)
        {
            ArgumentNullException.ThrowIfNull(logger);
            ArgumentNullException.ThrowIfNull(eventHandler);

            _logger = logger;
            _eventHandler = eventHandler;
        }

        public async Task Consume(ConsumeContext<FaultIntegrationEvent> context)
        {
            var jsonMessage = JsonConvert.SerializeObject(context.Message);
            _logger.LogInformation("Order declined. Order: {order}", jsonMessage);

            await _eventHandler.Process(context.Message);
        }
    }
}

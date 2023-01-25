using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WareHouse.IntegrationEvents;

namespace Warehouse.ProductService.Application.Consumers
{
    public class OrderFinishedConsumer : IConsumer<OrderFinishedIntegrationEvent>
    {
        private readonly ILogger<OrderFinishedConsumer> _logger;

        public OrderFinishedConsumer(ILogger<OrderFinishedConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<OrderFinishedIntegrationEvent> context)
        {
            var jsonMessage = JsonConvert.SerializeObject(context.Message);
            _logger.LogInformation("Order finished. Order: {order}", jsonMessage);
        }
    }
}

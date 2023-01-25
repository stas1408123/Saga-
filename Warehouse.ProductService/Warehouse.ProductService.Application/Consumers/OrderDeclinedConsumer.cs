using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WareHouse.IntegrationEvents;

namespace Warehouse.ProductService.Application.Consumers
{
    public class OrderDeclinedConsumer : IConsumer<OrderDeclinedIntegrationEvent>
    {
        private readonly ILogger<OrderDeclinedConsumer> _logger;

        public OrderDeclinedConsumer(ILogger<OrderDeclinedConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<OrderDeclinedIntegrationEvent> context)
        {
            var jsonMessage = JsonConvert.SerializeObject(context.Message);
            _logger.LogInformation("Order declined. Order: {order}", jsonMessage);
        }
    }
}

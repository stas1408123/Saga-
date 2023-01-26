using Microsoft.Extensions.Logging;
using WareHouse.IntegrationEvents;
using WareHouse.OrderService.Application.Contracts.Handlers;
using WareHouse.OrderService.Application.Contracts.Services;

namespace WareHouse.OrderService.Application.IntegrationEvents.Handlers
{
    public class ProductOutOfStockHandler : IIntegrationEventHandler<ProductOutOfStockIntegrationEvent>
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<ProductOutOfStockHandler> _logger;

        public ProductOutOfStockHandler(IOrderService orderService, ILogger<ProductOutOfStockHandler> logger)
        {
            ArgumentNullException.ThrowIfNull(orderService);
            ArgumentNullException.ThrowIfNull(logger);

            _orderService = orderService;
            _logger = logger;
        }

        public async Task Process(ProductOutOfStockIntegrationEvent @event, CancellationToken cancellationToken = default)
        {
            var product = @event.Payload;
            var order = await _orderService.GetById(product.OrderId, cancellationToken);

            if (order is null)
            {
                _logger.LogError("Order is null. Order: {id} process for product id: {productId}", order.Id, product.Id);
                return;
            }

            _logger.LogInformation("Order: {id} for product id: {productId}. Product is out of stock now. Need await until LowStock", order.Id, product.Id);
        }
    }
}

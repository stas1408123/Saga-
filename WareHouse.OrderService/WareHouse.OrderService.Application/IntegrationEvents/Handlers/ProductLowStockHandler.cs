using Microsoft.Extensions.Logging;
using WareHouse.IntegrationEvents;
using WareHouse.OrderService.Application.Contracts.Handlers;
using WareHouse.OrderService.Application.Contracts.Services;
using WareHouse.OrderService.Domain.Enums;

namespace WareHouse.OrderService.Application.IntegrationEvents.Handlers
{
    public class ProductLowStockHandler : IIntegrationEventHandler<ProductLowStockIntegrationEvent>
    {

        private readonly IOrderService _orderService;
        private readonly ILogger<ProductLowStockHandler> _logger;

        public ProductLowStockHandler(IOrderService orderService, ILogger<ProductLowStockHandler> logger)
        {
            ArgumentNullException.ThrowIfNull(orderService);
            ArgumentNullException.ThrowIfNull(logger);

            _orderService = orderService;
            _logger = logger;
        }

        public async Task Process(ProductLowStockIntegrationEvent @event, CancellationToken cancellationToken = default)
        {
            var product = @event.Payload;
            var order = await _orderService.GetById(product.OrderId!, cancellationToken);

            if (order is null)
            {
                _logger.LogError("Order is null. Order: {id} process for product id: {productId}", order!.Id, product.Id);
                return;
            }

            _logger.LogInformation("Put order: {id} in review process for product id: {productId}", order.Id, product.Id);

            var updatedOrder = await _orderService.ChangeStatus(order.Id, OrderStatus.InReview, cancellationToken);

            _logger.LogInformation("Finished changing order status to InReview for product id: {id}", product.Id);
        }
    }
}

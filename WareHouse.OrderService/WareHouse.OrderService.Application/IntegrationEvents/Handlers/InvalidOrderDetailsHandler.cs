using Microsoft.Extensions.Logging;
using WareHouse.IntegrationEvents;
using WareHouse.OrderService.Application.Contracts.Handlers;
using WareHouse.OrderService.Application.Contracts.Services;
using WareHouse.OrderService.Domain.Enums;

namespace WareHouse.OrderService.Application.IntegrationEvents.Handlers
{
    public class InvalidOrderDetailsHandler : IIntegrationEventHandler<InvalidOrderDetailsIntegrationEvent>
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<InvalidOrderDetailsHandler> _logger;

        public InvalidOrderDetailsHandler(IOrderService orderService, ILogger<InvalidOrderDetailsHandler> logger)
        {
            ArgumentNullException.ThrowIfNull(orderService);
            ArgumentNullException.ThrowIfNull(logger);

            _orderService = orderService;
            _logger = logger;
        }

        public async Task Process(InvalidOrderDetailsIntegrationEvent @event, CancellationToken cancellationToken = default)
        {
            var order = await _orderService.GetById(@event.Payload.Id, cancellationToken);

            if (order is null)
            {
                _logger.LogError("Order is null. Order: {id} process for product id: {productId}", order!.Id, order.ProductId);
                return;
            }

            _logger.LogError("Start marking order: {id} as FAILED for product id: {productId}", order!.Id, order.ProductId);

            await _orderService.ChangeStatus(order.Id, OrderStatus.Failed, cancellationToken);
        }
    }
}

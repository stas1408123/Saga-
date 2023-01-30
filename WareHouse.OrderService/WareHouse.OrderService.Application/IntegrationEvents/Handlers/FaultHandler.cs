using Microsoft.Extensions.Logging;
using WareHouse.IntegrationEvents;
using WareHouse.OrderService.Application.Contracts.Handlers;
using WareHouse.OrderService.Application.Contracts.Services;
using WareHouse.OrderService.Domain.Enums;

namespace WareHouse.OrderService.Application.IntegrationEvents.Handlers
{
    public class FaultHandler : IIntegrationEventHandler<FaultIntegrationEvent>
    {
        private IOrderService _orderService;
        private readonly ILogger<FaultHandler> _logger;

        public FaultHandler(IOrderService orderService, ILogger<FaultHandler> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        public async Task Process(FaultIntegrationEvent @event, CancellationToken cancellationToken = default)
        {
            var order = @event.Payload.Order;

            _logger.LogWarning("Order: {id} for product {productId}. Something faulted. Reverting changes...", order?.Id, order.ProductId);

            await _orderService.ChangeStatus(order.Id, (OrderStatus)order.OrderStatus, cancellationToken);
        }
    }
}

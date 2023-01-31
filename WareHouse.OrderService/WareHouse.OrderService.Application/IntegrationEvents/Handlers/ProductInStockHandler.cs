﻿using Microsoft.Extensions.Logging;
using WareHouse.IntegrationEvents;
using WareHouse.OrderService.Application.Contracts.Handlers;
using WareHouse.OrderService.Application.Contracts.Services;
using WareHouse.OrderService.Domain.Enums;

namespace Warehouse.OrderService.Application.IntegrationEvents.Handlers
{
    public class ProductInStockHandler : IIntegrationEventHandler<ProductInStockIntegrationEvent>
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<ProductInStockHandler> _logger;

        public ProductInStockHandler(IOrderService orderService, ILogger<ProductInStockHandler> logger)
        {
            ArgumentNullException.ThrowIfNull(orderService);
            ArgumentNullException.ThrowIfNull(logger);

            _orderService = orderService;
            _logger = logger;
        }

        public async Task Process(ProductInStockIntegrationEvent @event, CancellationToken cancellationToken = default)
        {
            var product = @event.Payload;
            var order = await _orderService.GetById(product.OrderId!, cancellationToken);

            if (order is null)
            {
                _logger.LogError("Order is null. Approve order: {id} process for product id: {productId}", order!.Id, product.Id);
                return;
            }

            _logger.LogInformation("Approve order: {id} process for product id: {productId}", order.Id, product.Id);

            await _orderService.ChangeStatus(order.Id, OrderStatus.Approved, cancellationToken);

            _logger.LogInformation("Finished order process for product id: {id}", product.Id);
        }
    }
}

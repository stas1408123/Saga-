using AutoMapper;
using Microsoft.Extensions.Logging;
using Warehouse.ProductService.Application.Contracts.Handlers;
using Warehouse.ProductService.Application.Contracts.Services;
using Warehouse.ProductService.Application.Models;
using WareHouse.IntegrationEvents;

namespace Warehouse.ProductService.Application.IntegrationEvents.Handlers
{
    public class OrderDeclinedHandler : IIntegrationEventHandler<OrderDeclinedIntegrationEvent>
    {
        private readonly ILogger<OrderDeclinedHandler> _logger;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public OrderDeclinedHandler(ILogger<OrderDeclinedHandler> logger, IProductService productService, IMapper mapper)
        {
            ArgumentNullException.ThrowIfNull(logger);
            ArgumentNullException.ThrowIfNull(productService);
            ArgumentNullException.ThrowIfNull(mapper);

            _logger = logger;
            _productService = productService;
            _mapper = mapper;
        }

        public async Task Process(OrderDeclinedIntegrationEvent @event, CancellationToken cancellationToken = default)
        {
            var order = @event.Payload;
            var orderDetails = _mapper.Map<OrderDetails>(order);

            _logger.LogInformation("Order declined event. Order id: {id} for product {productId}. Returning product from reservation.", order.Id, order.ProductId);

            var product = await _productService.CancelProductReservation(orderDetails, cancellationToken);

            _logger.LogInformation("Order declined event. Order id: {id} for product {productId}. Canceled product reservation.", order.Id, product.Id);
        }
    }
}

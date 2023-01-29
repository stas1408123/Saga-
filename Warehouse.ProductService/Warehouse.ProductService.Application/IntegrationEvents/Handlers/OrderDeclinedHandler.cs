using AutoMapper;
using MassTransit;
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
        private readonly IPublishEndpoint _publishEndpoint;

        public OrderDeclinedHandler(ILogger<OrderDeclinedHandler> logger, IProductService productService, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            ArgumentNullException.ThrowIfNull(logger);
            ArgumentNullException.ThrowIfNull(productService);
            ArgumentNullException.ThrowIfNull(mapper);
            ArgumentNullException.ThrowIfNull(publishEndpoint);

            _logger = logger;
            _productService = productService;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Process(OrderDeclinedIntegrationEvent @event, CancellationToken cancellationToken = default)
        {
            var order = @event.Payload;
            var orderDetails = _mapper.Map<OrderDetails>(order);

            _logger.LogInformation("Order declined event. Order id: {id} for product {productId}. Returning product from reservation.", order.Id, order.ProductId);

            var product = await _productService.CancelProductReservation(orderDetails, cancellationToken);

            _logger.LogInformation("Order declined event. Order id: {id} for product {productId}. Canceled product reservation.", order.Id, product.Id);

            await _publishEndpoint.Publish(new OrderFinishedIntegrationEvent(order), cancellationToken);
        }
    }
}

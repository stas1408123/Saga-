using AutoMapper;
using Microsoft.Extensions.Logging;
using Warehouse.ProductService.Application.Contracts.Handlers;
using Warehouse.ProductService.Application.Contracts.Services;
using WareHouse.IntegrationEvents;

namespace Warehouse.ProductService.Application.IntegrationEvents.Handlers
{

    public class OrderInReviewHandler : IIntegrationEventHandler<OrderInReviewIntegrationEvent>
    {
        private readonly ILogger<OrderInReviewHandler> _logger;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public OrderInReviewHandler(ILogger<OrderInReviewHandler> logger, IProductService productService, IMapper mapper)
        {
            ArgumentNullException.ThrowIfNull(logger);
            ArgumentNullException.ThrowIfNull(productService);
            ArgumentNullException.ThrowIfNull(mapper);

            _logger = logger;
            _productService = productService;
            _mapper = mapper;
        }

        public Task Process(OrderInReviewIntegrationEvent @event, CancellationToken cancellationToken = default)
        {
            var order = @event.Payload;
            _logger.LogInformation("Order in review event. Order id: {id} for product {productId}.", order.Id, order.ProductId);

            return Task.CompletedTask;
        }
    }
}

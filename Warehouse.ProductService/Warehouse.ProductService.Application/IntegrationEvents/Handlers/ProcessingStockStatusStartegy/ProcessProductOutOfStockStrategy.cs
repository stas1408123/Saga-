using MassTransit;
using Microsoft.Extensions.Logging;
using Warehouse.Contracts.DTOs;
using Warehouse.Contracts.DTOs.Enums;
using Warehouse.ProductService.Application.Contracts.Services;
using Warehouse.ProductService.Application.Contracts.Strategy;
using Warehouse.ProductService.Application.Models;
using WareHouse.IntegrationEvents;

namespace Warehouse.ProductService.Application.IntegrationEvents.Handlers.ProcessingOrderStartegy
{
    public class ProcessProductOutOfStockStrategy : IProcessingStockStatusStartegy
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<ProcessProductOutOfStockStrategy> _logger;
        private readonly IProductService _productService;

        public ProcessProductOutOfStockStrategy(IPublishEndpoint publishEndpoint, ILogger<ProcessProductOutOfStockStrategy> logger, IProductService productService)
        {
            ArgumentNullException.ThrowIfNull(publishEndpoint);
            ArgumentNullException.ThrowIfNull(logger);
            ArgumentNullException.ThrowIfNull(productService);

            _publishEndpoint = publishEndpoint;
            _logger = logger;
            _productService = productService;
        }

        public bool IsApplictable(StockStatus status) => status is StockStatus.OutOfStock;

        public async Task ProcessProduct(ProductDTO product, OrderDetails orderDetails, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Process order: {id}. Product is out of stock. Product id: {productId}", product.OrderId, product.Id);

            var integrationEvent = new ProductOutOfStockIntegrationEvent(product);
            await _publishEndpoint.Publish(integrationEvent, cancellationToken);

            _logger.LogInformation("Process order: {id}. Published product out of stock event: {productId}", product.OrderId, product.Id);
        }
    }
}

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
    public class ProcessProductInStockStrategy : IProcessingStockStatusStartegy
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<ProcessProductInStockStrategy> _logger;
        private readonly IProductService _productService;

        public ProcessProductInStockStrategy(IPublishEndpoint publishEndpoint, IProductService productService, ILogger<ProcessProductInStockStrategy> logger)
        {
            ArgumentNullException.ThrowIfNull(publishEndpoint);
            ArgumentNullException.ThrowIfNull(logger);
            ArgumentNullException.ThrowIfNull(productService);

            _publishEndpoint = publishEndpoint;
            _logger = logger;
            _productService = productService;
        }

        public bool IsApplictable(StockStatus status) => status is StockStatus.InStock;

        public async Task ProcessProduct(ProductDTO product, OrderDetails orderDetails, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Process order: {id}. Product is in stock. Reserving product id: {productId}", product.OrderId, product.Id);

            await _productService.ReserveProduct(orderDetails, cancellationToken);

            var integrationEvent = new ProductInStockIntegrationEvent(product);
            await _publishEndpoint.Publish(integrationEvent, cancellationToken);

            _logger.LogInformation("Process order: {id}. Published product in stock event: {productId}", product.OrderId, product.Id);
        }
    }
}

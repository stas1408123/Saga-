using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using Warehouse.Contracts.DTOs;
using Warehouse.ProductService.Application.Contracts.Handlers;
using Warehouse.ProductService.Application.Contracts.Services;
using WareHouse.IntegrationEvents;
using WarehouseService.Domain.Enums;

namespace Warehouse.ProductService.Application.IntegrationEvents.Handlers
{
    public class OrderStartedHandler : IIntegrationEventHandler<OrderStartedIntegrationEvent>
    {
        private readonly IProductService _productService;
        private readonly ILogger<OrderStartedHandler> _logger;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;

        public OrderStartedHandler(IProductService productService, ILogger<OrderStartedHandler> logger, IPublishEndpoint publishEndpoint, IMapper mapper)
        {
            ArgumentNullException.ThrowIfNull(productService);
            ArgumentNullException.ThrowIfNull(logger);
            ArgumentNullException.ThrowIfNull(publishEndpoint);
            ArgumentNullException.ThrowIfNull(mapper);

            _productService = productService;
            _logger = logger;
            _publishEndpoint = publishEndpoint;
            _mapper = mapper;
        }

        public async Task Process(OrderStartedIntegrationEvent @event, CancellationToken cancellationToken = default)
        {
            var order = @event.Payload;
            var product = await _productService.GetById(order.ProductId, cancellationToken);

            if (product is null)
            {
                _logger.LogInformation("Process order started. No product with id: {id}", @event.Payload.ProductId);
                return;
            }

            switch (product.StockStatus)
            {
                case StockStatus.InStock:
                    {
                        _logger.LogInformation("Process order: {id}. Product is in stock. Reserved product id: {productId}", order.Id, order.ProductId);
                        var integrationEvent = new ProductInStockIntegrationEvent(_mapper.Map<ProductDTO>(product));
                        _mapper.Map(order, integrationEvent);

                        await _publishEndpoint.Publish(integrationEvent, cancellationToken);
                        _logger.LogInformation("Process order: {id}. Published product in stock event: {productId}", order.Id, product.Id);

                        break;
                    }
                case StockStatus.LowStock:
                    {
                        _logger.LogInformation("Process order: {id}. Product is in low stock. Reserved product. Product id: {productId}", order.Id, order.ProductId);
                        await _publishEndpoint.Publish(new ProductLowStockIntegrationEvent(_mapper.Map<ProductDTO>(product)), cancellationToken);
                        _logger.LogInformation("Process order: {id}. Published product low stock event: {productId}", order.Id, product.Id);

                        break;
                    }
                case StockStatus.OutOfStock:
                    {
                        _logger.LogInformation("Process order: {id}. Product is out of stock. Product id: {productId}", order.Id, order.ProductId);
                        await _publishEndpoint.Publish(new ProductOutOfStockIntegrationEvent(_mapper.Map<ProductDTO>(product)), cancellationToken);
                        _logger.LogInformation("Process order: {id}. Published product out of stock event: {productId}", order.Id, product.Id);

                        break;
                    }
            }
        }
    }
}

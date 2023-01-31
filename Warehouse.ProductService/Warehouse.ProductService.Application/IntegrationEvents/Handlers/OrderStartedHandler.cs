using AutoMapper;
using Microsoft.Extensions.Logging;
using Warehouse.Contracts.DTOs;
using Warehouse.ProductService.Application.Contracts.Handlers;
using Warehouse.ProductService.Application.Contracts.Services;
using Warehouse.ProductService.Application.Contracts.Strategy;
using Warehouse.ProductService.Application.Infrastructure.Exceptions;
using Warehouse.ProductService.Application.Models;
using WareHouse.IntegrationEvents;

namespace Warehouse.ProductService.Application.IntegrationEvents.Handlers
{
    public class OrderStartedHandler : IIntegrationEventHandler<OrderStartedIntegrationEvent>
    {
        private readonly IProductService _productService;
        private readonly ILogger<OrderStartedHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IEnumerable<IProcessingStockStatusStartegy> _processingStockStatusStartegies;

        public OrderStartedHandler(IProductService productService,
            ILogger<OrderStartedHandler> logger,
            IMapper mapper,
            IEnumerable<IProcessingStockStatusStartegy> processingStockStatusStartegies)
        {
            ArgumentNullException.ThrowIfNull(productService);
            ArgumentNullException.ThrowIfNull(logger);
            ArgumentNullException.ThrowIfNull(mapper);
            ArgumentNullException.ThrowIfNull(processingStockStatusStartegies);

            _productService = productService;
            _logger = logger;
            _mapper = mapper;
            _processingStockStatusStartegies = processingStockStatusStartegies;
        }

        public async Task Process(OrderStartedIntegrationEvent @event, CancellationToken cancellationToken = default)
        {
            var order = @event.Payload;
            var product = await _productService.GetById(order.ProductId, cancellationToken);

            if (product == null)
            {
                _logger.LogError("Order id: {id}. Product with id: {productId} is null", order.Id, order.ProductId);

                throw new InvalidOrderDetailsException();
            }

            var orderDetails = _mapper.Map<OrderDetails>(order);

            var productDTO = _mapper.Map<ProductDTO>(product);
            _mapper.Map(order, productDTO);

            var strategy = _processingStockStatusStartegies.FirstOrDefault(x => x.IsApplictable(productDTO.StockStatus));
            await strategy!.ProcessProduct(productDTO, orderDetails, cancellationToken);
        }
    }
}

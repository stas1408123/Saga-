﻿using Microsoft.Extensions.Logging;
using Warehouse.ProductService.Application.Contracts.Handlers;
using Warehouse.ProductService.Application.Contracts.Repositories;
using WareHouse.IntegrationEvents;

namespace Warehouse.ProductService.Application.IntegrationEvents.Handlers
{
    public class FaultHandler : IIntegrationEventHandler<FaultIntegrationEvent>
    {
        private IProductRepository _productRepository;
        private readonly ILogger<FaultHandler> _logger;

        public FaultHandler(IProductRepository productRepository, ILogger<FaultHandler> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public Task Process(FaultIntegrationEvent @event, CancellationToken cancellationToken = default)
        {
            var payload = @event.Payload;

            _logger.LogWarning("Ordering process faulted. Payload: {payload}", payload);

            return Task.CompletedTask;
        }
    }
}

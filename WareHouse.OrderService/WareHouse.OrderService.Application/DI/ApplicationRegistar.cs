﻿using Microsoft.Extensions.DependencyInjection;
using Warehouse.OrderService.Application.IntegrationEvents.Handlers;
using WareHouse.IntegrationEvents;
using WareHouse.OrderService.Application.Contracts.Handlers;
using WareHouse.OrderService.Application.Contracts.Services;
using WareHouse.OrderService.Application.Contracts.Strategy;
using WareHouse.OrderService.Application.IntegrationEvents.Handlers;
using WareHouse.OrderService.Application.Mapper;
using WareHouse.OrderService.Application.Services.Strategy;

namespace WareHouse.OrderService.Application.DI
{
    public static class ApplicationRegistar
    {
        public static void AddApplicationDependencies(this IServiceCollection services)
        {
            services.AddTransient<IOrderService, Services.OrderService>();
            services.AddAutoMapper(typeof(EntityModelProfile), typeof(ModelDTOProfile));

            services.AddTransient<IIntegrationEventHandler<ProductInStockIntegrationEvent>, ProductInStockHandler>();
            services.AddTransient<IIntegrationEventHandler<ProductLowStockIntegrationEvent>, ProductLowStockHandler>();
            services.AddTransient<IIntegrationEventHandler<ProductOutOfStockIntegrationEvent>, ProductOutOfStockHandler>();
            services.AddTransient<IIntegrationEventHandler<InvalidOrderDetailsIntegrationEvent>, InvalidOrderDetailsHandler>();
            services.AddTransient<IIntegrationEventHandler<FaultIntegrationEvent>, FaultHandler>();

            services.AddTransient<IChangeOrderStatusStrategy, DeclineOrderStrategy>();
            services.AddTransient<IChangeOrderStatusStrategy, ApproveOrderStrategy>();
            services.AddTransient<IChangeOrderStatusStrategy, PutOrderInReviewStrategy>();
            services.AddTransient<IChangeOrderStatusStrategy, PutOrderInPendingStrategy>();
            services.AddTransient<IChangeOrderStatusStrategy, MarkOrderAsFailedStrategy>();
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using Warehouse.ProductService.Application.Contracts.Handlers;
using Warehouse.ProductService.Application.Contracts.Services;
using Warehouse.ProductService.Application.Contracts.Strategy;
using Warehouse.ProductService.Application.IntegrationEvents.Handlers;
using Warehouse.ProductService.Application.IntegrationEvents.Handlers.ProcessingOrderStartegy;
using Warehouse.ProductService.Application.Mapper;
using Warehouse.ProductService.Application.Services;
using WareHouse.IntegrationEvents;

namespace Warehouse.ProductService.Application.DI
{
    public static class ApplicationRegistar
    {
        public static void AddApplicationDependencies(this IServiceCollection services)
        {
            services.AddTransient<IProductService, Services.ProductService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IOrderDetailsService, OrderDetailsService>();
            services.AddAutoMapper(typeof(EntityModelProfile), typeof(ModelDTOProfile));

            services.AddTransient<IIntegrationEventHandler<OrderStartedIntegrationEvent>, OrderStartedHandler>();
            services.AddTransient<IIntegrationEventHandler<OrderFinishedIntegrationEvent>, OrderFinishedHandler>();
            services.AddTransient<IIntegrationEventHandler<OrderApprovedIntegrationEvent>, OrderApprovedHandler>();
            services.AddTransient<IIntegrationEventHandler<OrderDeclinedIntegrationEvent>, OrderDeclinedHandler>();
            services.AddTransient<IIntegrationEventHandler<OrderInReviewIntegrationEvent>, OrderInReviewHandler>();

            services.AddTransient<IProcessingStockStatusStartegy, ProcessProductInStockStrategy>();
            services.AddTransient<IProcessingStockStatusStartegy, ProcessProductOutOfStockStrategy>();
            services.AddTransient<IProcessingStockStatusStartegy, ProcessProductLowStockStrategy>();
        }
    }
}

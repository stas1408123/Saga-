using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using WareHouse.OrderService.Application.Consumers;
using WareHouse.OrderService.Application.Contracts.Contexts;
using WareHouse.OrderService.Application.Contracts.Repositories;
using WareHouse.OrderService.Infrastructure.Contexts;
using WareHouse.OrderService.Infrastructure.Options;
using WareHouse.OrderService.Infrastructure.Repositories;

namespace WareHouse.OrderService.Infrastructure.DI
{
    public static class InfrastructureRegistar
    {
        public static void AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.ConfigureOptions<MongoDBOptionsSetup>();

            services.AddScoped<IMongoDBContext, MongoDBContext>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddMassTransit(x =>
            {
                x.AddConsumer<ProductInStockConsumer>();
                x.AddConsumer<ProductOutOfStockConsumer>();
                x.AddConsumer<ProductLowStockConsumer>();

                x.SetKebabCaseEndpointNameFormatter();
                x.UsingRabbitMq((context, config) =>
                {
                    config.Host("localhost", "/", host =>
                    {
                        host.Username("guest");
                        host.Password("guest");
                    });

                    config.ConfigureEndpoints(context);

                    config.UseDelayedRedelivery(r => r.Intervals(TimeSpan.FromSeconds(15), TimeSpan.FromSeconds(30)));
                    config.UseMessageRetry(r => r.Immediate(2));
                    config.UseInMemoryOutbox();
                });
            });


        }
    }
}

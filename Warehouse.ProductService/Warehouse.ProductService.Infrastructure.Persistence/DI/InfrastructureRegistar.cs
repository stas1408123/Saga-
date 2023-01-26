using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Warehouse.ProductService.Application.Consumers;
using Warehouse.ProductService.Application.Contracts.Repositories;
using Warehouse.ProductService.Infrastructure.Repositories;
using WarehouseService.Application.Contracts.Repositories;
using WarehouseService.Domain.Entities;
using WarehouseService.Infrastructure.Repositories;

namespace WarehouseService.Infrastructure.DI
{
    public static class InfrastructureRegistar
    {
        public static void AddInfrastructureDependencies(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseNpgsql(config.GetConnectionString("DbConnection"));
            });

            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();
                x.AddConsumer<OrderStartedConsumer>();
                x.AddConsumer<OrderFinishedConsumer>();
                x.AddConsumer<OrderApprovedConsumer>();
                x.AddConsumer<OrderDeclinedConsumer>();
                x.AddConsumer<OrderInReviewConsumer>();

                x.UsingRabbitMq((context, config) =>
                {
                    config.Host("localhost", "/", host =>
                    {
                        host.Username("guest");
                        host.Password("guest");
                    });

                    config.ConfigureEndpoints(context);
                });
            });

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IGenericRepository<CategoryEntity>, GenericRepository<CategoryEntity>>();
        }
    }
}

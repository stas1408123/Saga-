using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WareHouse.OrderService.Application.Contracts.Contexts;
using WareHouse.OrderService.Application.Contracts.Repositories;
using WareHouse.OrderService.Infrastructure.Persistence.Contexts;
using WareHouse.OrderService.Infrastructure.Persistence.Options;
using WareHouse.OrderService.Infrastructure.Persistence.Repositories;

namespace WareHouse.OrderService.Infrastructure.Persistence.DI
{
    public static class PersistenceRegistar
    {
        public static void AddPersistenceDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDBOptions>(options => configuration.GetSection(MongoDBOptions.MongoSettings));

            services.AddScoped<IMongoDBContext, MongoDBContext>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq();
                x.SetKebabCaseEndpointNameFormatter();
                x.UsingRabbitMq((context, cfg) => cfg.ConfigureEndpoints(context));
            });

        }
    }
}

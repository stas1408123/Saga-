﻿using MassTransit;
using Microsoft.Extensions.DependencyInjection;
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
                x.SetKebabCaseEndpointNameFormatter();
                x.UsingRabbitMq((context, config) =>
                {
                    config.Host("localhost", "/", host =>
                    {
                        host.Username("guest");
                        host.Password("guest");
                    });

                    config.ConfigureEndpoints(context);

                    //config.ReceiveEndpoint("order-input-queue", e =>
                    //{
                    //    e.Bind("order-process-exchange");
                    //});

                    //config.Publish<OrderStartedIntegrationEvent>(x => { x.ExchangeType = ExchangeType.Fanout; x.Exclude = true; x.BindQueue("order-process-exchange", "order-input-queue"); });
                    //config.Publish<OrderDeclinedIntegrationEvent>(x => { x.ExchangeType = ExchangeType.Fanout; x.Exclude = true; x.BindQueue("order-process-exchange", "order-input-queue"); });
                    //config.Publish<OrderFinishedIntegrationEvent>(x => { x.ExchangeType = ExchangeType.Fanout; x.Exclude = true; x.BindQueue("order-process-exchange", "order-input-queue"); });
                    //config.Publish<OrderApprovedIntegrationEvent>(x => { x.ExchangeType = ExchangeType.Fanout; x.Exclude = true; x.BindQueue("order-process-exchange", "order-input-queue"); });
                    //config.Publish<OrderInReviewIntegrationEvent>(x => { x.ExchangeType = ExchangeType.Fanout; x.Exclude = true; x.BindQueue("order-process-exchange", "order-input-queue"); });
                });
            });
        }
    }
}

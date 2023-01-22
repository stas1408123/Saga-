using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WareHouse.OrderService.Application.Contracts.Services;

namespace WareHouse.OrderService.Application.DI
{
    public static class ApplicationRegistar
    {
        public static void AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IOrderService, Services.OrderService>();
        }
    }
}

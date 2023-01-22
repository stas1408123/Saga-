using Microsoft.Extensions.DependencyInjection;
using WareHouse.OrderService.Application.Contracts.Services;

namespace WareHouse.OrderService.Application.DI
{
    public static class ApplicationRegistar
    {
        public static void AddApplicationDependencies(this IServiceCollection services)
        {
            services.AddTransient<IOrderService, Services.OrderService>();
        }
    }
}

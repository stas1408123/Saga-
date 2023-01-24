using Microsoft.Extensions.DependencyInjection;
using WareHouse.OrderService.Application.Contracts.Services;
using WareHouse.OrderService.Application.Mapper;

namespace WareHouse.OrderService.Application.DI
{
    public static class ApplicationRegistar
    {
        public static void AddApplicationDependencies(this IServiceCollection services)
        {
            services.AddTransient<IOrderService, Services.OrderService>();
            services.AddAutoMapper(typeof(EntityModelProfile), typeof(ModelDTOProfile));
        }
    }
}

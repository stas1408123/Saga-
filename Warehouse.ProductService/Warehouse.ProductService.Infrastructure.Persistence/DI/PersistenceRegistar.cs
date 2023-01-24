using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Warehouse.ProductService.Application.Contracts.Repositories;
using Warehouse.ProductService.Infrastructure.Persistence.Repositories;

namespace WarehouseService.Infrastructure.Persistence.DI
{
    public static class PersistenceRegistar
    {
        public static void AddPersistenceDependencies(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseNpgsql(config.GetConnectionString("DbConnection"));
            });

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WarehouseService.Application.Contracts.Repositories;
using WarehouseService.Domain.Entities.Base;
using WarehouseService.Infrastructure.Persistance.Repositories;

namespace WarehouseService.Infrastructure.Persistance.DI
{
    public static class PersistanceRegistar
    {
        public static void AddPersistenceDependencies(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseNpgsql(config.GetConnectionString("DbConnection"));
            });

            services.AddScoped<IGenericRepository<EntityBase>, GenericRepository<EntityBase>>();
        }
    }
}

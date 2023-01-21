using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WarehouseService.Application.Contracts.Repositories;
using WarehouseService.Domain.Entities.Base;
using WarehouseService.Infrastructure.Persistence.Repositories;

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

            services.AddScoped<IGenericRepository<EntityBase>, GenericRepository<EntityBase>>();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using WarehouseService.Domain.Entities;
using WarehouseService.Infrastructure.Persistance.EntitiesConfiguration;

namespace WarehouseService.Infrastructure.Persistance
{
    public sealed class DatabaseContext : DbContext
    {
        public DbSet<OrderEntity>? Orders { get; set; }
        public DbSet<CategoryEntity>? Categories { get; set; }
        public DbSet<ProductEntity>? Products { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OrderEntityConfiguration());
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseService.Domain.Entities;

namespace WarehouseService.Infrastructure.Persistance.EntitiesConfiguration
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.Property(x => x.StockStatus).HasConversion<string>();
        }
    }
}

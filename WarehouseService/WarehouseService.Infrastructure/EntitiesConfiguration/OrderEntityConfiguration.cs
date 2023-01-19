using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WarehouseService.Domain.Entities;

namespace WarehouseService.Infrastructure.Persistance.EntitiesConfiguration
{
    public class OrderEntityConfiguration : IEntityTypeConfiguration<OrderEntity>
    {
        public void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            builder.Property(x => x.OrderStatus).HasConversion<string>();
        }
    }
}

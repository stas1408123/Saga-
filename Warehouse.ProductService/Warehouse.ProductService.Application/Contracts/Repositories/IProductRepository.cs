using WarehouseService.Application.Contracts.Repositories;
using WarehouseService.Domain.Entities;

namespace Warehouse.ProductService.Application.Contracts.Repositories
{
    public interface IProductRepository : IGenericRepository<ProductEntity>
    {
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}

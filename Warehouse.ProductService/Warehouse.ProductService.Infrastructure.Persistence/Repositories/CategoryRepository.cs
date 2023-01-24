using Warehouse.ProductService.Application.Contracts.Repositories;
using WarehouseService.Domain.Entities;
using WarehouseService.Infrastructure.Persistence;
using WarehouseService.Infrastructure.Persistence.Repositories;

namespace Warehouse.ProductService.Infrastructure.Persistence.Repositories
{
    public class CategoryRepository : GenericRepository<CategoryEntity>, ICategoryRepository
    {
        public CategoryRepository(DatabaseContext context) : base(context) { }
    }
}

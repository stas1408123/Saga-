using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Warehouse.ProductService.Application.Contracts.Repositories;
using WarehouseService.Domain.Entities;
using WarehouseService.Infrastructure;
using WarehouseService.Infrastructure.Repositories;

namespace Warehouse.ProductService.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<ProductEntity>, IProductRepository
    {
        public ProductRepository(DatabaseContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<ProductEntity>> Get(CancellationToken cancellationToken)
        {
            return await _dbSet.AsNoTracking().Include(x => x.Category).ToListAsync(cancellationToken);
        }

        public override async Task<IEnumerable<ProductEntity>> GetByPredicate(Expression<Func<ProductEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            var entities = await Query.AsNoTracking().Where(predicate).Include(x => x.Category).ToListAsync(cancellationToken);

            return entities;
        }
    }
}

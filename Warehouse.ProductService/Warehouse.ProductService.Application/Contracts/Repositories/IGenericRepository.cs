using System.Linq.Expressions;
using WarehouseService.Domain.Entities.Base;

namespace WarehouseService.Application.Contracts.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : EntityBase
    {
        Task<TEntity> Insert(TEntity entity, CancellationToken cancellationToken);
        Task<IEnumerable<TEntity>> Get(CancellationToken cancellationToken);
        Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken);
        Task<TEntity> Delete(int id, CancellationToken cancellationToken);
        Task<IEnumerable<TEntity>> GetByPredicate(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
        void RevertChanges();
    }
}

using System.Linq.Expressions;
using WareHouse.OrderService.Domain.Entities.Base;

namespace WareHouse.OrderService.Application.Contracts.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : EntityBase
    {
        Task<TEntity> Insert(TEntity entity, CancellationToken cancellationToken);
        Task<IEnumerable<TEntity>> Get(CancellationToken cancellationToken);
        Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken);
        Task<int> Delete(string id, CancellationToken cancellationToken);
        Task<IEnumerable<TEntity>> GetByPredicate(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
        Task<int> DeleteRange(List<string> ids, CancellationToken cancellationToken);
    }
}

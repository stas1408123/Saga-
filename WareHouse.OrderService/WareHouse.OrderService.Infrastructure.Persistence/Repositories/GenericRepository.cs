using MongoDB.Driver;
using System.Linq.Expressions;
using WareHouse.OrderService.Application.Contracts.Contexts;
using WareHouse.OrderService.Application.Contracts.Repositories;
using WareHouse.OrderService.Domain.Entities.Base;

namespace WareHouse.OrderService.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : EntityBase
    {
        protected readonly IMongoCollection<TEntity> _collection;

        protected GenericRepository(IMongoDBContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            _collection = context.GetCollection<TEntity>();
        }

        public async Task<int> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _collection.DeleteOneAsync(x => x.Id == id);

            return (int)result.DeletedCount;
        }

        public async Task<IEnumerable<TEntity>> Get(CancellationToken cancellationToken)
        {
            var entities = await _collection.FindAsync(x => true, cancellationToken: cancellationToken);
            var result = await entities.ToListAsync(cancellationToken);

            return result;
        }

        public async Task<IEnumerable<TEntity>> GetByPredicate(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            var entities = await _collection.FindAsync(predicate, cancellationToken: cancellationToken);
            var result = await entities.ToListAsync(cancellationToken);

            return result;
        }

        public async Task<TEntity> Insert(TEntity entity, CancellationToken cancellationToken)
        {
            await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);

            return entity;
        }

        public async Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken)
        {
            await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity, cancellationToken: cancellationToken);

            return entity;
        }
    }
}

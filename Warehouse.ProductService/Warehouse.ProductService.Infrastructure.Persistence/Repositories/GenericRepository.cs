using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WarehouseService.Application.Contracts.Repositories;
using WarehouseService.Domain.Entities.Base;

namespace WarehouseService.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : EntityBase
    {
        protected readonly DatabaseContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected IQueryable<TEntity> Query => _dbSet.AsQueryable();

        public GenericRepository(DatabaseContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual async Task<TEntity> Insert(TEntity entity, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public virtual async Task<IEnumerable<TEntity>> GetByPredicate(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            var entities = await Query.AsNoTracking().Where(predicate).ToListAsync(cancellationToken);

            return entities;
        }

        public async Task<IEnumerable<TEntity>> InsertRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            await _dbSet.AddRangeAsync(entities, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return entities;
        }

        public async Task<TEntity> Delete(int id, CancellationToken cancellationToken)
        {
            var entities = await GetByPredicate(x => x.Id == id, cancellationToken);
            var entity = entities.FirstOrDefault();

            _context.Entry(entity!).State = EntityState.Deleted;
            await _context.SaveChangesAsync(cancellationToken);

            return entity!;
        }

        public virtual async Task<IEnumerable<TEntity>> Get(CancellationToken cancellationToken)
        {
            return await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public void RevertChanges()
        {
            var changedEntries = _context.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged).ToList();

            foreach (var entry in changedEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            }
        }
    }
}

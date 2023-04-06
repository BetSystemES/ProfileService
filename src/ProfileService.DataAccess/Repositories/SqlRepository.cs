using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProfileService.BusinessLogic.Contracts.DataAccess.Repositories;

namespace ProfileService.DataAccess.Repositories
{
    public class SqlRepository<TEntity> : IDataRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _entities;

        protected SqlRepository(DbContext dbContext)
        {
            _entities = dbContext.Set<TEntity>();
            _dbContext = dbContext;
        }

        public EntityEntry Entry(TEntity entity)
        {
            return _dbContext.Entry(entity);
        }

        public EntityEntry Attach(TEntity entity)
        {
            return _dbContext.Attach(entity);
        }

        public void Detach(TEntity entity)
        {
            var entry = _dbContext.Entry(entity);

            if (entry.State != EntityState.Detached)
                entry.State = EntityState.Detached;
        }

        public virtual async Task Add(TEntity entity, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(entity, "entity");
            await _entities.AddAsync(entity, token);
        }

        public virtual async Task AddRange(IEnumerable<TEntity> entities, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(entities, "entities");
            await _entities.AddRangeAsync(entities, token);
        }

        public virtual Task Remove(TEntity entity, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(entity, "entity");
            _entities.Remove(entity);
            return Task.CompletedTask;
        }

        public virtual Task RemoveRange(IEnumerable<TEntity> entities, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(entities, "entity");
            _entities.RemoveRange(entities);
            return Task.CompletedTask;
        }

        public virtual Task Update(TEntity entity, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(entity, "entity");
            _entities.Update(entity);
            return Task.CompletedTask;
        }

        public virtual Task UpdateRange(IEnumerable<TEntity> entities, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(entities, "entities");
            _entities.UpdateRange(entities);
            return Task.CompletedTask;
        }
    }
}
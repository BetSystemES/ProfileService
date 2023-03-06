using Microsoft.EntityFrameworkCore;

namespace ProfileService.DataAccess.Repositories
{
    public class SqlRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _entities;

        protected SqlRepository(DbSet<TEntity> entities)
        {
            _entities = entities;
        }

        public virtual Task Add(TEntity entity, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(entity, "entity");
            _entities.Add(entity);
            return Task.CompletedTask;
        }

        public virtual Task AddRange(IEnumerable<TEntity> entities, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(entities, "entities");
            var entities2 = (entities as List<TEntity>) ?? entities.ToList();
            _entities.AddRange(entities2);
            return Task.CompletedTask;
        }

        public virtual Task Remove(TEntity entity, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(entity, "entity");
            _entities.Remove(entity);
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
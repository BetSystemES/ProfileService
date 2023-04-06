using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ProfileService.BusinessLogic.Contracts.DataAccess.Repositories
{
    public interface IDataRepository<in TEntity>
    {
        Task Add(TEntity entity, CancellationToken token);

        Task AddRange(IEnumerable<TEntity> entities, CancellationToken token);

        Task Remove(TEntity entity, CancellationToken token);

        Task RemoveRange(IEnumerable<TEntity> entities, CancellationToken token);

        Task Update(TEntity entity, CancellationToken token);

        Task UpdateRange(IEnumerable<TEntity> entities, CancellationToken token);

        public EntityEntry Attach(TEntity entity);

        public void Detach(TEntity entity);

        public EntityEntry Entry(TEntity entity);
    }
}
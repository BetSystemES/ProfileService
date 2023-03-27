using ProfileService.BusinessLogic.Contracts.DataAccess.Filters;
using ProfileService.BusinessLogic.Entities;
using System.Linq.Expressions;

namespace ProfileService.BusinessLogic.Contracts.DataAccess.Providers
{
    public interface IBonusProvider : IFilterProvider<Bonus>
    {
        Task<Bonus> Get(Guid id, CancellationToken cancellationToken);
        public Task<int> GetCount(Expression<Func<Bonus, bool>> predicate, CancellationToken cancellationToken);
        public Task<List<Bonus>> GetPaged(Expression<Func<Bonus, bool>> predicate, Func<IQueryable<Bonus>, IOrderedQueryable<Bonus>> order, int? skip, int? take, CancellationToken cancellationToken);
    }
}

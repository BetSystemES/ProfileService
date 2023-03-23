using ProfileService.BusinessLogic.Entities;
using System.Linq.Expressions;

namespace ProfileService.BusinessLogic.Contracts.DataAccess.Providers
{
    public interface IBonusFinder : IProvider<Bonus>, IFilter<Bonus>
    {
        public Task<List<Bonus>> GetPaged(Expression<Func<Bonus, bool>> predicate, Func<IQueryable<Bonus>, IOrderedQueryable<Bonus>> order, int? skip, int? take, CancellationToken cancellationToken);
    }
}

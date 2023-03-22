using System.Linq.Expressions;
using ProfileService.BusinessLogic.Models;

namespace ProfileService.BusinessLogic.Contracts.DataAccess.Providers
{
    public interface IFilter<T> where T : class
    {
        Task<List<T>> FindBy(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
        Task<List<T>> FindByPageFilter(Expression<Func<T, bool>> predicate, PageFilter pageFilter, CancellationToken cancellationToken);
    }
}
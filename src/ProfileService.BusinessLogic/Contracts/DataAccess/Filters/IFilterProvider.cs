using System.Linq.Expressions;

namespace ProfileService.BusinessLogic.Contracts.DataAccess.Filters
{
    public interface IFilterProvider<T> where T : class
    {
        Task<List<T>> FindBy(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    }
}
using System.Linq.Expressions;

namespace ProfileService.BusinessLogic.Contracts.DataAccess.Providers
{
    public interface IFilter<T> where T : class
    {
        Task<List<T>> FindBy(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    }
}
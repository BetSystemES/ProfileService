namespace ProfileService.BusinessLogic.Contracts.DataAccess.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task Add(T item, CancellationToken token);
        Task Update(T item, CancellationToken cancellationToken);
    }
}

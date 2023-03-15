namespace ProfileService.BusinessLogic.Contracts.DataAccess.Providers
{
    public interface IProvider<T> where T : class
    {
        Task<T> Get(Guid id, CancellationToken cancellationToken);
    }
}
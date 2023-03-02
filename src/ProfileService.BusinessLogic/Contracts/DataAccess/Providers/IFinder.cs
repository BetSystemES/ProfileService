namespace ProfileService.BusinessLogic.Contracts.DataAccess.Providers
{
    public interface IFinder<T> where T : class
    {
        Task<List<T>> FindByProfileId(Guid id, CancellationToken cancellationToken);
    }
}

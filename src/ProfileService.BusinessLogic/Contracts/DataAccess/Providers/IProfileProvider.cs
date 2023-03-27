using ProfileService.BusinessLogic.Entities;

namespace ProfileService.BusinessLogic.Contracts.DataAccess.Providers
{
    public interface IProfileProvider
    {
        Task<ProfileData> Get(Guid id, CancellationToken cancellationToken);
    }
}
using ProfileService.BusinessLogic.Entities;

namespace ProfileService.BusinessLogic.Contracts.DataAccess.Repositories
{
    public interface IProfileRepository : IDataRepository<ProfileData>
    {
    }
}
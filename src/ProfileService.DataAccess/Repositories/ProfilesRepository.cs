using Microsoft.EntityFrameworkCore;
using ProfileService.BusinessLogic.Contracts.DataAccess.Repositories;
using ProfileService.BusinessLogic.Entities;

namespace ProfileService.DataAccess.Repositories
{
    public class ProfilesRepository : SqlRepository<ProfileData>, IProfileRepository
    {
        public ProfilesRepository(DbSet<ProfileData> entities) : base(entities)
        {
        }
    }
}
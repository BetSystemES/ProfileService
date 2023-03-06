using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProfileService.BusinessLogic.Contracts.DataAccess.Repositories;
using ProfileService.BusinessLogic.Entities;

namespace ProfileService.DataAccess.Repositories
{
    public class ProfilesRepository : SqlRepository<PersonalData>, IProfileRepository
    {
        public ProfilesRepository(DbSet<PersonalData> entities) : base(entities)
        {
        }
    }
}
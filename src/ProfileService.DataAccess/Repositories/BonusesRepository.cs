using Microsoft.EntityFrameworkCore;
using ProfileService.BusinessLogic.Contracts.DataAccess.Repositories;
using ProfileService.BusinessLogic.Entities;

namespace ProfileService.DataAccess.Repositories
{
    public class BonusesRepository : SqlRepository<Bonus>, IBonusRepository
    {
        public BonusesRepository(ProfileDbContext dbContext) : base(dbContext)
        {
        }
    }
}
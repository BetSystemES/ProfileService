using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProfileService.BusinessLogic.Contracts.DataAccess.Repositories;
using ProfileService.BusinessLogic.Entities;

namespace ProfileService.DataAccess.Repositories
{
    public class BonusesRepository : SqlRepository<Bonus>, IBonusRepository
    {
        public BonusesRepository(DbSet<Bonus> entities) : base(entities)
        {
        }
    }
}
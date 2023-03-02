using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProfileService.BusinessLogic.Contracts.DataAccess.Repositories;
using ProfileService.BusinessLogic.Entities;

namespace ProfileService.DataAccess.Repositories
{
    public class BonusesRepository : IRepository<Bonus>
    {
        private readonly DbSet<Bonus> _entities;
        private readonly ILogger<BonusesRepository> _logger;

        public BonusesRepository(DbSet<Bonus> entities, ILogger<BonusesRepository> logger)
        {
            _entities = entities;
            _logger = logger;
        }

        public Task Add(Bonus item, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(item, nameof(item));
            var result = _entities.Add(item);
             _logger.LogTrace("Add Bonus item with BonusId:{bonusId} and PersonalId:{personalId} to database",item.BonusId, item.PersonalId );
             return Task.CompletedTask;
        }

        public Task Update(Bonus item, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(item, nameof(item));
            _entities.Update(item);
            _logger.LogTrace("Update Bonus item with BonusId:{bonusId} and PersonalId:{personalId} in database", item.BonusId, item.PersonalId);
            return Task.CompletedTask;
        }
    }
}

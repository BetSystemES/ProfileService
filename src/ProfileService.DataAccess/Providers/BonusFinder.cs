using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProfileService.BusinessLogic.Contracts.DataAccess.Providers;
using ProfileService.BusinessLogic.Entities;

namespace ProfileService.DataAccess.Providers
{
    public class BonusFinder : IFinder<Bonus>, IProvider<Bonus>
    {
        private readonly DbSet<Bonus> _entities;

        private readonly ILogger<BonusFinder> _logger;

        public BonusFinder(DbSet<Bonus> entities, ILogger<BonusFinder> logger)
        {
            _entities = entities;
            _logger = logger;
        }

        public async Task<Bonus> Get(Guid guid, CancellationToken cancellationToken)
        {
            var result = await _entities.FindAsync(guid);
            _logger.LogTrace("Get Bonus item from database by Id:{guid}", guid);
            return result;
        }


        public async Task<List<Bonus>> FindByProfileId(Guid id, CancellationToken cancellationToken)
        {
            var result = await _entities.Where(x => x.ProfileId == id).ToListAsync(cancellationToken: cancellationToken);
            _logger.LogTrace("Find bonuses from database by profile Id:{id}. Count={Count}", id, result.Count);
            return result;
        }
    }
}
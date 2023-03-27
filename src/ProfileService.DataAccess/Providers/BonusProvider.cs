using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProfileService.BusinessLogic.Contracts.DataAccess.Providers;
using ProfileService.BusinessLogic.Entities;
using System.Linq.Expressions;

using ProfileService.BusinessLogic.Extensions;

namespace ProfileService.DataAccess.Providers
{
    public class BonusProvider : IBonusProvider
    {
        private readonly DbSet<Bonus> _entities;

        private readonly ILogger<BonusProvider> _logger;

        public BonusProvider(DbSet<Bonus> entities, ILogger<BonusProvider> logger)
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

        public async Task<List<Bonus>> FindBy(Expression<Func<Bonus, bool>> predicate,
            CancellationToken cancellationToken)
        {
            var result = await _entities.Where(predicate).ToListAsync(cancellationToken: cancellationToken);
            _logger.LogTrace("Find bonuses from database by predicate. Count={Count}", result.Count);
            return result;
        }

        public async Task<List<Bonus>> GetPaged(Expression<Func<Bonus, bool>> predicate, Func<IQueryable<Bonus>, IOrderedQueryable<Bonus>> order, int? skip, int? take, CancellationToken cancellationToken)
        {
            var result = await _entities.Where(predicate)
                .OrderByFunc(order)
                .SkipTake(skip, take)
                .ToListAsync(cancellationToken: cancellationToken);
            _logger.LogTrace(
                "Find bonuses from database by predicate and by page with params: skip={skip} and take={take}. Count={Count}",
                skip, take, result.Count);
            return result;
        }

        public async Task<int> GetCount(Expression<Func<Bonus, bool>> predicate)
        {
            var result = await _entities.Where(predicate).CountAsync();
            return result;
        }
    }
}
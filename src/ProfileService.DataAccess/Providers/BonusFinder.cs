using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProfileService.BusinessLogic.Contracts.DataAccess.Providers;
using ProfileService.BusinessLogic.Entities;
using System.Linq.Expressions;
using ProfileService.BusinessLogic.Models;

namespace ProfileService.DataAccess.Providers
{
    public class BonusFinder : IProvider<Bonus>, IFilter<Bonus>
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

        public async Task<List<Bonus>> FindBy(Expression<Func<Bonus, bool>> predicate,
            CancellationToken cancellationToken)
        {
            var result = await _entities.Where(predicate).ToListAsync(cancellationToken: cancellationToken);
            _logger.LogTrace("Find bonuses from database by predicate. Count={Count}", result.Count);
            return result;
        }

        public async Task<List<Bonus>> FindByPageFilter(Expression<Func<Bonus, bool>> predicate, PaginationCriteria paginationCriteria,
            CancellationToken cancellationToken)
        {
            if (paginationCriteria == null)
            {
                var resultWoFilter = await _entities.Where(predicate)
                    .ToListAsync(cancellationToken: cancellationToken);
                _logger.LogTrace("Find bonuses from database only by predicate because pageFilter==null. Count={Count}", resultWoFilter.Count);
                return resultWoFilter;
            }
            
            int page = paginationCriteria.PageNumber;
            int pageSize = paginationCriteria.PageSize;

            var result = await _entities.Where(predicate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken: cancellationToken);
            _logger.LogTrace(
                "Find bonuses from database by predicate and by page with params: page={page} and pageSize={pageSize}. Count={Count}",
                page, pageSize, result.Count);
            return result;
        }
    }
}
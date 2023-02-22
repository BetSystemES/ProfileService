using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfileService.BusinessLogic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ProfileService.DataAccess.Providers
{
    internal class BonusProvider : IProvider<Bonus>
    {
        private readonly DbSet<Bonus> _entities;

        private readonly ILogger<BonusProvider> _logger;

        public BonusProvider(DbSet<Bonus> entities, ILogger<BonusProvider> logger)
        {
            _entities = entities;
            _logger = logger;
        }

        public async Task<List<Bonus>> FindByProfileId(Guid id, CancellationToken cancellationToken)
        {
            //return _entities.Where(x => x.PersonalId == id).ToListAsync(cancellationToken: cancellationToken);

            var result = await _entities.Where(x => x.PersonalId == id).ToListAsync(cancellationToken: cancellationToken);

            _logger.LogTrace("Find bonuses from database by profile Id:{id}. Count={Count}", id, result.Count);

            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfileService.BusinessLogic;
using Microsoft.EntityFrameworkCore;

namespace ProfileService.DataAccess.Providers
{
    internal class BonusProvider : IProvider<Bonus>
    {
        private readonly DbSet<Bonus> _entities;

        public BonusProvider(DbSet<Bonus> entities)
        {
            _entities = entities;
        }

        public Task<IEnumerable<Bonus>> FindByProfileId(Guid id, CancellationToken cancellationToken)
        {
            return Task.FromResult(_entities.Where(x => x.PersonalId == id).ToListAsync(cancellationToken: cancellationToken).Result.AsEnumerable());
        }
    }
}

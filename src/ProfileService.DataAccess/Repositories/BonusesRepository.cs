using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProfileService.BusinessLogic;
using ProfileService.DataAccess.EF;
using ProfileService.DataAccess.Interfaces;

namespace ProfileService.DataAccess.Repositories
{
    public class BonusesRepositiry : IRepository<Bonus>, IProvider<Bonus>
    {
        private readonly DbSet<Bonus> _entities;

        public BonusesRepositiry(DbSet<Bonus> entities)
        {
            _entities = entities;
        }

        public Task<IEnumerable<Bonus>> FindByProfileId(Guid id, CancellationToken cancellationToken)
        {
            return Task.FromResult(_entities.Where(x => x.PersonalId == id).ToListAsync(cancellationToken: cancellationToken).Result.AsEnumerable());
        }

        public Task<Bonus> Get(int id, CancellationToken cancellationToken)
        {
            return Task.FromResult(_entities.FindAsync(id).Result);
        }

        public Task Update(Bonus item, CancellationToken cancellationToken)
        {
            _entities.Update(item);
            return Task.CompletedTask;
        }
    }
}

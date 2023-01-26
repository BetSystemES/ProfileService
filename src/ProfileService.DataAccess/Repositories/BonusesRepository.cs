using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProfileService.BusinessLogic;
using ProfileService.DataAccess.EF;

namespace ProfileService.DataAccess.Repositories
{
    public class BonusesRepositiry : IRepository<Bonus>
    {
        private readonly DbSet<Bonus> _entities;

        public BonusesRepositiry(DbSet<Bonus> entities)
        {
            _entities = entities;
        }

        public async Task<Bonus> Get(Guid guid, CancellationToken cancellationToken)
        {
             //return Task.FromResult(_entities.FindAsync(guid.ToString()).Result);
             return await _entities.FindAsync(guid.ToString());
        }

        public Task Update(Bonus item, CancellationToken cancellationToken)
        {
            _entities.Update(item);
            return Task.CompletedTask;
        }
    }
}

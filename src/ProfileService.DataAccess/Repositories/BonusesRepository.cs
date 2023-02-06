using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProfileService.BusinessLogic;
using ProfileService.DataAccess.EF;

namespace ProfileService.DataAccess.Repositories
{
    public class BonusesRepositiry : IRepository<Bonus>
    {
        private readonly DbSet<Bonus> _entities;
        private readonly ILogger<BonusesRepositiry> _logger;

        public BonusesRepositiry(DbSet<Bonus> entities, ILogger<BonusesRepositiry> logger)
        {
            _entities = entities;
            _logger = logger;
        }

        public Task Add(Bonus item, CancellationToken token)
        {
             var result = _entities.Add(item);
             _logger.LogTrace("Add Bonus item with BonusId:{bonusId} and PersonalId:{personalId} to database",item.BonusId, item.PersonalId );
             return Task.CompletedTask;
        }

        public async Task<Bonus> Get(Guid guid, CancellationToken cancellationToken)
        {
             //return Task.FromResult(_entities.FindAsync(guid.ToString()).Result);
              var result=await _entities.FindAsync(guid);
              _logger.LogTrace("Get Bonus item from database by Id:{guid}", guid);
              return result;
        }

        public Task Update(Bonus item, CancellationToken cancellationToken)
        {
            _entities.Update(item);
            _logger.LogTrace("Update Bonus item with BonusId:{bonusId} and PersonalId:{personalId} in database", item.BonusId, item.PersonalId);
            return Task.CompletedTask;
        }
    }
}

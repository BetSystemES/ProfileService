using ProfileService.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfileService.DataAccess.EF;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using Microsoft.Extensions.Logging;

namespace ProfileService.DataAccess.Repositories
{
    public class ProfilesRepositiry : IRepository<PersonalData>
    {
        private readonly DbSet<PersonalData> _entities;
        private readonly ILogger<ProfilesRepositiry> _logger;

        public ProfilesRepositiry(DbSet<PersonalData> entities, ILogger<ProfilesRepositiry> logger )
        {
            _entities = entities;
            _logger = logger;
        }

        public Task Add(PersonalData item, CancellationToken token)
        {
            _entities.Add(item);
            _logger.LogTrace("Add PersonalData item to database");
            return Task.CompletedTask;
        }

        public async Task<PersonalData> Get(Guid guid, CancellationToken cancellationToken)
        {
            var result = await _entities.FindAsync(guid);
            _logger.LogTrace("Get PersonalData item from database by Id:{guid}", guid);
            return result;
        }

        public Task Update(PersonalData item, CancellationToken cancellationToken)
        {
            _entities.Update(item);
            _logger.LogTrace("Update PersonalData item in database");
            return Task.CompletedTask;
        }
    }
}
using ProfileService.BusinessLogic;
// TODO: remove unused/sort usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfileService.DataAccess.EF;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using Microsoft.Extensions.Logging;
using ProfileService.EntityModels.Models;

namespace ProfileService.DataAccess.Repositories
{
    public class ProfilesRepository : IRepository<PersonalData>
    {
        private readonly DbSet<PersonalData> _entities;
        private readonly ILogger<ProfilesRepository> _logger;

        public ProfilesRepository(DbSet<PersonalData> entities, ILogger<ProfilesRepository> logger )
        {
            _entities = entities;
            _logger = logger;
        }

        public Task Add(PersonalData item, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(item, nameof(item));
            _entities.Add(item);
            _logger.LogTrace("Add PersonalData item with Id:{guid} to database", item.PersonalId );
            return Task.CompletedTask;
        }

        public Task Update(PersonalData item, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(item, nameof(item));
            _entities.Update(item);
            _logger.LogTrace("Update PersonalData item  with Id:{guid} in database", item.PersonalId);
            return Task.CompletedTask;
        }
    }
}
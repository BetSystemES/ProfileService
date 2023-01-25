using ProfileService.BusinessLogic;
using ProfileService.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfileService.DataAccess.EF;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace ProfileService.DataAccess.Repositories
{
    public class ProfilesRepositiry : IRepository<PersonalData>
    {
        private readonly DbSet<PersonalData> _entities;

        public ProfilesRepositiry(DbSet<PersonalData> entities)
        {
            _entities = entities;
        }

        public Task<PersonalData> Get(int id, CancellationToken cancellationToken)
        {
            return Task.FromResult(_entities.FindAsync(id).Result);
        }

        public Task Update(PersonalData item, CancellationToken cancellationToken)
        {
            _entities.Update(item);
            return Task.CompletedTask;
        }
    }
}
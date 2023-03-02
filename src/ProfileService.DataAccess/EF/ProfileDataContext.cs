// TODO: remove unused/sort usings
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using ProfileService.BusinessLogic;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.DataAccess.Configuration;


// TODO: remove all empty lines
namespace ProfileService.DataAccess.EF
{
    // TODO: change file location to ProfileService.DataAccess
    public class ProfileDataContext : IDataContext
    {
        private readonly ProfileDbContext _profileDbContext;

        public ProfileDataContext(ProfileDbContext profileDbContext)
        {
            _profileDbContext = profileDbContext;
        }

        /// <summary>Saves the changes.</summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>
        ///   Save changes result
        /// </returns>
        public Task SaveChanges(CancellationToken token)
        {
            return _profileDbContext.SaveChangesAsync(token);
        }
    }

}
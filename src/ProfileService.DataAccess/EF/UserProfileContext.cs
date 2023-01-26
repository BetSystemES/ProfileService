using Microsoft.EntityFrameworkCore;
using System.Numerics;
using ProfileService.BusinessLogic;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProfileService.DataAccess.Configuration;


namespace ProfileService.DataAccess.EF
{
    public class UserProfileContext : IDataContext
    {
        private readonly ProfileContext _profileContext;

        public UserProfileContext(ProfileContext profileContext)
        {
            _profileContext = profileContext;
        }

        /// <summary>Saves the changes.</summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>
        ///   Save changes result
        /// </returns>
        public Task SaveChanges(CancellationToken token)
        {
            return _profileContext.SaveChangesAsync(token);
        }
    }

}
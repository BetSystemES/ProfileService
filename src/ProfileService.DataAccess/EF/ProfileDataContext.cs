using ProfileService.BusinessLogic.Contracts.DataAccess;

namespace ProfileService.DataAccess.EF
{
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
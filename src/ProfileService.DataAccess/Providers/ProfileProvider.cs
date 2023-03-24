using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProfileService.BusinessLogic.Contracts.DataAccess.Providers;
using ProfileService.BusinessLogic.Entities;

namespace ProfileService.DataAccess.Providers
{
    public class ProfileProvider : IProfileProvider
    {
        private readonly DbSet<ProfileData> _entities;

        private readonly ILogger<ProfileData> _logger;

        public ProfileProvider(DbSet<ProfileData> entities, ILogger<ProfileData> logger)
        {
            _entities = entities;
            _logger = logger;
        }

        public async Task<ProfileData> Get(Guid guid, CancellationToken cancellationToken)
        {
            var result = await _entities.FindAsync(guid);
            _logger.LogTrace("Get ProfileData item from database by Id:{guid}", guid);
            return result;
        }
    }
}
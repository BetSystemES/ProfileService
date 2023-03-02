using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProfileService.BusinessLogic.Contracts.DataAccess.Providers;
using ProfileService.BusinessLogic.Entities;

namespace ProfileService.DataAccess.Providers
{
    public class PersonalDataProvider : IProvider<PersonalData>
    {
        private readonly DbSet<PersonalData> _entities;

        private readonly ILogger<PersonalData> _logger;

        public PersonalDataProvider(DbSet<PersonalData> entities, ILogger<PersonalData> logger)
        {
            _entities = entities;
            _logger = logger;
        }

        public async Task<PersonalData> Get(Guid guid, CancellationToken cancellationToken)
        {
            var result = await _entities.FindAsync(guid);
            _logger.LogTrace("Get PersonalData item from database by Id:{guid}", guid);
            return result;
        }
    }
}

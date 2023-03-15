using Microsoft.Extensions.DependencyInjection;
using FluentAssertions;
using ProfileService.BusinessLogic.Contracts.DataAccess.Repositories;
using ProfileService.BusinessLogic.Contracts.DataAccess;
using ProfileService.BusinessLogic.Contracts.DataAccess.Providers;
using ProfileService.BusinessLogic.Entities;
using static ProfileService.TestDataGeneratorsAndExtensions.DataGenerator;

namespace ProfileService.IntegrationTests.DataAccess.Providers
{
    public class ProfileServiceBonusProviderTests : IClassFixture<GrpcAppFactory>, IDisposable
    {
        private static readonly CancellationToken _ctoken = CancellationToken.None;

        private readonly IServiceScope _scope;
        private readonly IProfileRepository _profileDataRepository;
        private readonly IBonusRepository _bonusRepository;
        private readonly IFinder<Bonus> _bonusFinder;
        private readonly IDataContext _context;

        public ProfileServiceBonusProviderTests(GrpcAppFactory factory)
        {
            _scope = factory.Services.CreateScope();
            _profileDataRepository = _scope.ServiceProvider.GetRequiredService<IProfileRepository>();
            _bonusRepository = _scope.ServiceProvider.GetRequiredService<IBonusRepository>();
            _bonusFinder = _scope.ServiceProvider.GetRequiredService<IFinder<Bonus>>();
            _context = _scope.ServiceProvider.GetRequiredService<IDataContext>();
        }
        
        [Fact]
        public async Task FindByProfileId_Should_Return_Result()
        {
            // Arrange
            var profileId = Guid.NewGuid();
            ProfileData profileData = ProfileDataGenerator(profileId);

            var bonusId = Guid.NewGuid();
            Bonus bonus = BonusGenerator(bonusId, profileId);

            List<Bonus> expectedResult = new List<Bonus>() { bonus };

            // Act
            await _profileDataRepository.Add(profileData, _ctoken);
            await _context.SaveChanges(_ctoken);

            await _bonusRepository.Add(bonus, _ctoken);
            await _context.SaveChanges(_ctoken);

            var actualResult = await _bonusFinder.FindByProfileId(profileId, _ctoken);

            // Assert
            actualResult.Should()
                .NotBeNull().And
                .BeEquivalentTo(expectedResult);
        }

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}

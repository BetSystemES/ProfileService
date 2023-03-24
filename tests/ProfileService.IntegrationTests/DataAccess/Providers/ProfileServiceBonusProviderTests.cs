using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using ProfileService.BusinessLogic.Contracts.DataAccess;
using ProfileService.BusinessLogic.Contracts.DataAccess.Providers;
using ProfileService.BusinessLogic.Contracts.DataAccess.Repositories;
using ProfileService.BusinessLogic.Entities;
using ProfileService.BusinessLogic.Helpers;
using static ProfileService.TestDataGeneratorsAndExtensions.DataGenerator;

namespace ProfileService.IntegrationTests.DataAccess.Providers
{
    public class ProfileServiceBonusProviderTests : IClassFixture<GrpcAppFactory>, IDisposable
    {
        private static readonly CancellationToken _ctoken = CancellationToken.None;

        private readonly IServiceScope _scope;
        private readonly IProfileRepository _profileRepository;
        private readonly IBonusRepository _bonusRepository;
        private readonly IDataContext _context;

        private readonly IBonusProvider _bonusProvider;

        public ProfileServiceBonusProviderTests(GrpcAppFactory factory)
        {
            _scope = factory.Services.CreateScope();
            _profileRepository = _scope.ServiceProvider.GetRequiredService<IProfileRepository>();
            _bonusRepository = _scope.ServiceProvider.GetRequiredService<IBonusRepository>();
            _context = _scope.ServiceProvider.GetRequiredService<IDataContext>();
            _bonusProvider = _scope.ServiceProvider.GetRequiredService<IBonusProvider>();
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
            await _profileRepository.Add(profileData, _ctoken);
            await _context.SaveChanges(_ctoken);

            await _bonusRepository.Add(bonus, _ctoken);
            await _context.SaveChanges(_ctoken);

            var actualResult = await _bonusProvider.FindBy(x=>x.ProfileId==profileId, _ctoken);

            // Assert
            actualResult.Should()
                .NotBeNull().And
                .BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetPaged_Should_Return_Result()
        {
            // Arrange
            var profileId = Guid.NewGuid();
            ProfileData profileData = ProfileDataGenerator(profileId);

            var bonusId = Guid.NewGuid();
            Bonus bonus = BonusGenerator(bonusId, profileId);

            List<Bonus> expectedResult = new List<Bonus>() { bonus };

            // Act
            await _profileRepository.Add(profileData, _ctoken);
            await _context.SaveChanges(_ctoken);

            await _bonusRepository.Add(bonus, _ctoken);
            await _context.SaveChanges(_ctoken);

            var expression = PredicateBuilderHelper.True<Bonus>();

            var actualResult = await _bonusProvider.GetPaged(expression, null, 2, 5, CancellationToken.None);

            // Assert
            actualResult.Should()
                .NotBeNull();
        }

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}
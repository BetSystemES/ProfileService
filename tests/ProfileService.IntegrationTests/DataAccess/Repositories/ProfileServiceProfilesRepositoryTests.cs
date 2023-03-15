using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using ProfileService.BusinessLogic.Contracts.DataAccess;
using ProfileService.BusinessLogic.Contracts.DataAccess.Providers;
using ProfileService.BusinessLogic.Contracts.DataAccess.Repositories;
using ProfileService.BusinessLogic.Entities;
using ProfileService.TestDataGeneratorsAndExtensions.Extensions;
using static ProfileService.TestDataGeneratorsAndExtensions.DataGenerator;

namespace ProfileService.IntegrationTests.DataAccess.Repositories
{
    public class ProfileServiceProfilesRepositoryTests : IClassFixture<GrpcAppFactory>, IDisposable
    {
        private static readonly CancellationToken _ctoken = CancellationToken.None;

        private readonly IServiceScope _scope;

        private readonly IProfileRepository _pofileDataRepository;
        private readonly IBonusRepository _bonusRepository;
        private readonly IFinder<Bonus> _bonusFinder;
        private readonly IProvider<Bonus> _bonusProvider;
        private readonly IProvider<ProfileData> _profileDataProvider;

        private readonly IDataContext _context;

        public ProfileServiceProfilesRepositoryTests(GrpcAppFactory factory)
        {
            _scope = factory.Services.CreateScope();

            _pofileDataRepository = _scope.ServiceProvider.GetRequiredService<IProfileRepository>();
            _bonusRepository = _scope.ServiceProvider.GetRequiredService<IBonusRepository>();
            _bonusFinder = _scope.ServiceProvider.GetRequiredService<IFinder<Bonus>>();

            _bonusProvider = _scope.ServiceProvider.GetRequiredService<IProvider<Bonus>>();
            _profileDataProvider = _scope.ServiceProvider.GetRequiredService<IProvider<ProfileData>>();

            _context = _scope.ServiceProvider.GetRequiredService<IDataContext>();
        }

        [Fact]
        public async Task AddProfile_Should_Return_Result()
        {
            // Arrange
            var profileId = Guid.NewGuid();
            ProfileData expectedResult = ProfileDataGenerator(profileId);

            // Act
            await _pofileDataRepository.Add(expectedResult, _ctoken);
            await _context.SaveChanges(_ctoken);

            var actualResult = await _profileDataProvider.Get(profileId, _ctoken);

            // Assert
            actualResult.Should()
                .NotBeNull().And
                .BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task UpdateProfile_Should_Return_UpdatedResult()
        {
            // Arrange
            var profileId = Guid.NewGuid();
            ProfileData initialProfileData = ProfileDataGenerator(profileId);

            ProfileData expectedResult = ProfileDataGenerator(profileId)
                                            .ChangeSurname("P")
                                            .ChangePhoneNumber("111222333");

            // Act
            await _pofileDataRepository.Add(initialProfileData, _ctoken);
            await _context.SaveChanges(_ctoken);

            var actualResult = await _profileDataProvider.Get(profileId, _ctoken);

            actualResult.LastName = expectedResult.LastName;
            actualResult.PhoneNumber = expectedResult.PhoneNumber;

            await _pofileDataRepository.Update(actualResult, _ctoken);
            await _context.SaveChanges(_ctoken);

            actualResult = await _profileDataProvider.Get(profileId, _ctoken);

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
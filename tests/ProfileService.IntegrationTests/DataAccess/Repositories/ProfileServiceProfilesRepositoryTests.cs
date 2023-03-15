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

        private readonly IProfileRepository _personalDataRepository;
        private readonly IBonusRepository _bonusRepository;
        private readonly IFinder<Bonus> _bonusFinder;
        private readonly IProvider<Bonus> _bonusProvider;
        private readonly IProvider<ProfileData> _personalDataProvider;

        private readonly IDataContext _context;

        public ProfileServiceProfilesRepositoryTests(GrpcAppFactory factory)
        {
            _scope = factory.Services.CreateScope();

            _personalDataRepository = _scope.ServiceProvider.GetRequiredService<IProfileRepository>();
            _bonusRepository = _scope.ServiceProvider.GetRequiredService<IBonusRepository>();
            _bonusFinder = _scope.ServiceProvider.GetRequiredService<IFinder<Bonus>>();

            _bonusProvider = _scope.ServiceProvider.GetRequiredService<IProvider<Bonus>>();
            _personalDataProvider = _scope.ServiceProvider.GetRequiredService<IProvider<ProfileData>>();

            _context = _scope.ServiceProvider.GetRequiredService<IDataContext>();
        }
        
        [Fact]
        public async Task AddProfile_Should_Return_Result()
        {
            // Arrange
            var personalId = Guid.NewGuid();
            ProfileData expectedResult = ProfileDataGenerator(personalId);

            // Act
            await _personalDataRepository.Add(expectedResult, _ctoken);
            await _context.SaveChanges(_ctoken);

            var actualResult = await _personalDataProvider.Get(personalId, _ctoken);

            // Assert
            actualResult.Should()
                .NotBeNull().And
                .BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task UpdateProfile_Should_Return_UpdatedResult()
        {
            // Arrange
            var personalId = Guid.NewGuid();
            ProfileData initialProfileData = ProfileDataGenerator(personalId);

            ProfileData expectedResult = ProfileDataGenerator(personalId)
                                            .ChangeSurname("P")
                                            .ChangePhoneNumber("111222333");

            // Act
            await _personalDataRepository.Add(initialProfileData, _ctoken);
            await _context.SaveChanges(_ctoken);

            var actualResult = await _personalDataProvider.Get(personalId, _ctoken);

            actualResult.LastName = expectedResult.LastName;
            actualResult.PhoneNumber = expectedResult.PhoneNumber;

            await _personalDataRepository.Update(actualResult, _ctoken);
            await _context.SaveChanges(_ctoken);

            actualResult = await _personalDataProvider.Get(personalId, _ctoken);

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
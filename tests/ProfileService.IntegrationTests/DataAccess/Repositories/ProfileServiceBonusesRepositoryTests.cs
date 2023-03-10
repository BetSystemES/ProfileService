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
    public class ProfileServiceBonusesRepositoryTests : IClassFixture<GrpcAppFactory>, IDisposable
    {
        private static readonly CancellationToken _ctoken = CancellationToken.None;

        private readonly IServiceScope _scope;
        private readonly IProfileRepository _personalDataRepository;
        private readonly IBonusRepository _bonusRepository;
        private readonly IFinder<Bonus> _bonusFinder;
        private readonly IProvider<Bonus> _bonusProvider;
        private readonly IProvider<PersonalData> _personalDataProvider;
        private readonly IDataContext _context;
        public ProfileServiceBonusesRepositoryTests(GrpcAppFactory factory)
        {
            _scope = factory.Services.CreateScope();
            _personalDataRepository = _scope.ServiceProvider.GetRequiredService<IProfileRepository>();
            _bonusRepository = _scope.ServiceProvider.GetRequiredService<IBonusRepository>();
            _bonusFinder = _scope.ServiceProvider.GetRequiredService<IFinder<Bonus>>();
            _bonusProvider = _scope.ServiceProvider.GetRequiredService<IProvider<Bonus>>();
            _personalDataProvider = _scope.ServiceProvider.GetRequiredService<IProvider<PersonalData>>();
            _context = _scope.ServiceProvider.GetRequiredService<IDataContext>();
        }
        
        [Fact]
        public async Task AddBonus_Should_Return_Result()
        {
            // Arrange
            var personalId = Guid.NewGuid();
            PersonalData personalData = PersonalDataGenerator(personalId);

            var bonusId = Guid.NewGuid();
            Bonus expectedResult = BonusGenerator(bonusId, personalId);

            // Act
            await _personalDataRepository.Add(personalData, _ctoken);
            await _bonusRepository.Add(expectedResult, _ctoken);
            await _context.SaveChanges(_ctoken);

            var actualResult = await _bonusProvider.Get(bonusId, _ctoken);

            // Assert
            actualResult.Should()
                .NotBeNull().And
                .BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task UpdateBonus_Should_Return_UpdatedResult()
        {
            // Arrange
            var personalId = Guid.NewGuid();
            PersonalData personalData = PersonalDataGenerator(personalId);

            var bonusId = Guid.NewGuid();
            Bonus initialBonus = BonusGenerator(bonusId, personalId ,personalData).ChangeisAlreadyUsed(false).ChangeAmount(50);
            Bonus expectedResult = BonusGenerator(bonusId, personalId, personalData).ChangeisAlreadyUsed(true).ChangeAmount(0);

            // Act
            await _personalDataRepository.Add(personalData, _ctoken);
            await _bonusRepository.Add(initialBonus, _ctoken);
            await _context.SaveChanges(_ctoken);

            var actualResult = await _bonusProvider.Get(bonusId, _ctoken);

            actualResult.isAlreadyUsed = true;
            actualResult.Amount = 0;

            await _bonusRepository.Update(actualResult, _ctoken);
            await _context.SaveChanges(_ctoken);

            actualResult = await _bonusProvider.Get(bonusId, _ctoken);

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
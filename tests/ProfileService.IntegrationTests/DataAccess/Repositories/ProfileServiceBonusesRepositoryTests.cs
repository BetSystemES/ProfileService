using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using ProfileService.BusinessLogic.Contracts.DataAccess;
using ProfileService.BusinessLogic.Contracts.DataAccess.Providers;
using ProfileService.BusinessLogic.Contracts.DataAccess.Repositories;
using ProfileService.BusinessLogic.Entities;
using ProfileService.BusinessLogic.Models.Enums;

namespace ProfileService.IntegrationTests.DataAccess.Repositories
{
    public class ProfileServiceBonusesRepositoryTests : IClassFixture<GrpcAppFactory>, IDisposable
    {
        private static readonly CancellationToken _ctoken = CancellationToken.None;

        private readonly IServiceScope _scope;

        private readonly IRepository<PersonalData> _personalDataRepository;
        private readonly IRepository<Bonus> _bonusRepository;
        private readonly IFinder<Bonus> _bonusFinder;

        private readonly IProvider<Bonus> _bonusProvider;
        private readonly IProvider<PersonalData> _personalDataProvider;

        private readonly IDataContext _context;
        public ProfileServiceBonusesRepositoryTests(GrpcAppFactory factory)
        {
            _scope = factory.Services.CreateScope();

            _personalDataRepository = _scope.ServiceProvider.GetRequiredService<IRepository<PersonalData>>();
            _bonusRepository = _scope.ServiceProvider.GetRequiredService<IRepository<Bonus>>();
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
            PersonalData personalData = new PersonalData()
            {
                PersonalId = personalId,
                Name = "Pavel",
                Surname = "K",
                PhoneNumber = "444333222",
                Email = "PavelK@google.com"
            };

            var bonusId = Guid.NewGuid();

            Bonus expectedResult = new Bonus()
            {
                BonusId = bonusId,
                PersonalId = personalId,
                isAlreadyUsed = true,
                DiscountType = DiscountType.Amount,
                Amount = 50,
                Discount = 30
            };

            // Act
            await _personalDataRepository.Add(personalData, _ctoken);
            await _context.SaveChanges(_ctoken);

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
            PersonalData personalData = new PersonalData()
            {
                PersonalId = personalId,
                Name = "Pavel",
                Surname = "K",
                PhoneNumber = "444333222",
                Email = "PavelK@google.com"
            };

            var bonusId = Guid.NewGuid();

            Bonus initialBonus = new Bonus()
            {
                BonusId = bonusId,
                PersonalId = personalId,
                PersonalData = personalData,
                isAlreadyUsed = false,
                DiscountType = DiscountType.Amount,
                Amount = 50,
                Discount = 30
            };

            Bonus expectedResult = new Bonus()
            {
                BonusId = bonusId,
                PersonalId = personalId,
                PersonalData = personalData,
                isAlreadyUsed = true,
                DiscountType = DiscountType.Amount,
                Amount = 0,
                Discount = 30
            };

            // Act
            await _personalDataRepository.Add(personalData, _ctoken);
            await _context.SaveChanges(_ctoken);

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

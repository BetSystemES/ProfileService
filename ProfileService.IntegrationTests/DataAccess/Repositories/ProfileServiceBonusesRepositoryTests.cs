using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using ProfileService.BusinessLogic;

namespace ProfileService.IntegrationTests.DataAccess.Repositories
{
    public class ProfileServiceBonusesRepositoryTests : IClassFixture<GrpcAppFactory>, IDisposable
    {
        private static readonly CancellationToken _ctoken = CancellationToken.None;

        private readonly IServiceScope _scope;

        private readonly IRepository<PersonalData> _personalDataRepository;
        private readonly IRepository<Bonus> _bonusRepository;
        private readonly IProvider<Bonus> _bonusProvider;

        private readonly IDataContext _context;
        public ProfileServiceBonusesRepositoryTests(GrpcAppFactory factory)
        {
            _scope = factory.Services.CreateScope();

            _personalDataRepository = _scope.ServiceProvider.GetRequiredService<IRepository<PersonalData>>();
            _bonusRepository = _scope.ServiceProvider.GetRequiredService<IRepository<Bonus>>();
            _bonusProvider = _scope.ServiceProvider.GetRequiredService<IProvider<Bonus>>();

            _context = _scope.ServiceProvider.GetRequiredService<IDataContext>();
        }
        
        [Fact]
        public async Task AddBonus_Should_Return_Result()
        {
            // Arrange
            Bonus expectedResult = new Bonus()
            {
                BonusId = Guid.Parse("34c92d2c-1f47-4a04-bffa-71101718b56d"),
                PersonalId = Guid.Parse("8f902da9-e152-4864-8b5d-3c36a3c6f496"),
                isAlreadyUsed = true,
                DiscountType = DiscountType.Amount,
                Amount = 50,
                Discount = 30
            };

            var bonusId = Guid.Parse("34c92d2c-1f47-4a04-bffa-71101718b56d");

            // Act

            //await _bonusRepository.Add(expectedResult, _ctoken);
            //await _context.SaveChanges(_ctoken);

            var actualResult = await _bonusRepository.Get(bonusId, _ctoken);

            // Assert
            actualResult.Should()
                .NotBeNull().And
                .BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task UpdateBonus_Should_Return_UpdatedResult()
        {
            // Arrange
            Bonus initialBonus = new Bonus()
            {
                BonusId = Guid.Parse("34c92d2c-1f47-4a04-bffa-71101718b56d"),
                PersonalId = Guid.Parse("8f902da9-e152-4864-8b5d-3c36a3c6f496"),
                isAlreadyUsed = false,
                DiscountType = DiscountType.Amount,
                Amount = 50,
                Discount = 30
            };

            Bonus expectedResult = new Bonus()
            {
                BonusId = Guid.Parse("34c92d2c-1f47-4a04-bffa-71101718b56d"),
                PersonalId = Guid.Parse("8f902da9-e152-4864-8b5d-3c36a3c6f496"),
                isAlreadyUsed = true,
                DiscountType = DiscountType.Amount,
                Amount = 50,
                Discount = 30
            };

            var bonusId = Guid.Parse("34c92d2c-1f47-4a04-bffa-71101718b56d");

            // Act
            //await _bonusRepository.Add(initialBonus, _ctoken);
            //await _context.SaveChanges(_ctoken);

            await _bonusRepository.Update(expectedResult, _ctoken);
            await _context.SaveChanges(_ctoken);

            var actualResult = await _bonusRepository.Get(bonusId, _ctoken);

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

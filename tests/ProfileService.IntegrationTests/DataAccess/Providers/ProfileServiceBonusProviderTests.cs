using Microsoft.Extensions.DependencyInjection;
using FluentAssertions;
using ProfileService.BusinessLogic.Contracts.DataAccess.Repositories;
using ProfileService.BusinessLogic.Contracts.DataAccess;
using ProfileService.BusinessLogic.Contracts.DataAccess.Providers;
using ProfileService.BusinessLogic.Entities;
using ProfileService.BusinessLogic.Models.Enums;

namespace ProfileService.IntegrationTests.DataAccess.Providers
{
    public class ProfileServiceBonusProviderTests : IClassFixture<GrpcAppFactory>, IDisposable
    {
        private static readonly CancellationToken _ctoken = CancellationToken.None;

        private readonly IServiceScope _scope;

        private readonly IProfileRepository _personalDataRepository;
        private readonly IBonusRepository _bonusRepository;
        private readonly IFinder<Bonus> _bonusFinder;

        private readonly IDataContext _context;
        public ProfileServiceBonusProviderTests(GrpcAppFactory factory)
        {
            _scope = factory.Services.CreateScope();

            _personalDataRepository = _scope.ServiceProvider.GetRequiredService<IProfileRepository>();
            _bonusRepository = _scope.ServiceProvider.GetRequiredService<IBonusRepository>();
            _bonusFinder = _scope.ServiceProvider.GetRequiredService<IFinder<Bonus>>();

            _context = _scope.ServiceProvider.GetRequiredService<IDataContext>();
        }
        
        [Fact]
        public async Task FindByProfileId_Should_Return_Result()
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

            Bonus bonus = new Bonus()
            {
                BonusId = bonusId,
                PersonalId = personalId,
                isAlreadyUsed = true,
                DiscountType = DiscountType.Amount,
                Amount = 50,
                Discount = 30
            };

            List<Bonus> expectedResult = new List<Bonus>() { bonus };

            // Act

            await _personalDataRepository.Add(personalData, _ctoken);
            await _context.SaveChanges(_ctoken);

            await _bonusRepository.Add(bonus, _ctoken);
            await _context.SaveChanges(_ctoken);

            var actualResult = await _bonusFinder.FindByProfileId(personalId, _ctoken);

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

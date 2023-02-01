using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using ProfileService.BusinessLogic;

namespace ProfileService.IntegrationTests.DataAccess.Providers
{
    public class ProfileServiceBonusProviderTests : IClassFixture<GrpcAppFactory>, IDisposable
    {
        private static readonly CancellationToken _ctoken = CancellationToken.None;

        private readonly IServiceScope _scope;

        private readonly IRepository<PersonalData> _personalDataRepository;
        private readonly IRepository<Bonus> _bonusRepository;
        private readonly IProvider<Bonus> _bonusProvider;

        private readonly IDataContext _context;
        public ProfileServiceBonusProviderTests(GrpcAppFactory factory)
        {
            _scope = factory.Services.CreateScope();

            _personalDataRepository = _scope.ServiceProvider.GetRequiredService<IRepository<PersonalData>>();
            _bonusRepository = _scope.ServiceProvider.GetRequiredService<IRepository<Bonus>>();
            _bonusProvider = _scope.ServiceProvider.GetRequiredService<IProvider<Bonus>>();

            _context = _scope.ServiceProvider.GetRequiredService<IDataContext>();
        }
        
        [Fact]
        public async Task FindByProfileId_Should_Return_Result()
        {
            // Arrange
            Bonus bonus = new Bonus()
            {
                BonusId = Guid.Parse("34c92d2c-1f47-4a04-bffa-71101718b56d"),
                PersonalId = Guid.Parse("8f902da9-e152-4864-8b5d-3c36a3c6f496"),
                isAlreadyUsed = true,
                DiscountType = DiscountType.Amount,
                Amount = 50,
                Discount = 30
            };

            var profileId = Guid.Parse("8f902da9-e152-4864-8b5d-3c36a3c6f496");

            List<Bonus> expectedResult = new List<Bonus>() { bonus };

            // Act

            //await _bonusRepository.Add(bonus, _ctoken);
            //await _context.SaveChanges(_ctoken);

            var actualResult = await _bonusProvider.FindByProfileId(profileId, _ctoken);

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

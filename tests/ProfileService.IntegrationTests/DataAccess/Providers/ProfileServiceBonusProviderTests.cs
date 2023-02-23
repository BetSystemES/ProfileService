using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using ProfileService.BusinessLogic;
using ProfileService.EntityModels.Models;

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

            var actualResult = await _bonusProvider.FindByProfileId(personalId, _ctoken);

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

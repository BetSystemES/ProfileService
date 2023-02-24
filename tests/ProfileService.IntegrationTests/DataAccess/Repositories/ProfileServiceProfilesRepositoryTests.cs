using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using ProfileService.BusinessLogic;
using ProfileService.EntityModels.Models;

namespace ProfileService.IntegrationTests.DataAccess.Repositories
{
    public class ProfileServiceProfilesRepositoryTests : IClassFixture<GrpcAppFactory>, IDisposable
    {
        private static readonly CancellationToken _ctoken = CancellationToken.None;

        private readonly IServiceScope _scope;

        private readonly IRepository<PersonalData> _personalDataRepository;
        private readonly IRepository<Bonus> _bonusRepository;
        private readonly IFinder<Bonus> _bonusFinder;
        private readonly IProvider<Bonus> _bonusProvider;
        private readonly IProvider<PersonalData> _personalDataProvider;

        private readonly IDataContext _context;

        public ProfileServiceProfilesRepositoryTests(GrpcAppFactory factory)
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
        public async Task AddProfile_Should_Return_Result()
        {
            // Arrange
            var personalId = Guid.NewGuid();
            PersonalData expectedResult = new PersonalData()
            {
                PersonalId = personalId,
                Name = "Pavel",
                Surname = "K",
                PhoneNumber = "444333222",
                Email = "PavelK@google.com"
            };

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

            PersonalData initialPersonalData = new PersonalData()
            {
                PersonalId = personalId,
                Name = "Pavel",
                Surname = "K",
                PhoneNumber = "444333222",
                Email = "PavelK@google.com"
            };

            PersonalData expectedResult = new PersonalData()
            {
                PersonalId = personalId,
                Name = "Pavel",
                Surname = "P",
                PhoneNumber = "111222333",
                Email = "PavelK@google.com"
            };

            // Act
            await _personalDataRepository.Add(initialPersonalData, _ctoken);
            await _context.SaveChanges(_ctoken);

            var actualResult = await _personalDataProvider.Get(personalId, _ctoken);

            actualResult.Surname= expectedResult.Surname;
            actualResult.PhoneNumber= expectedResult.PhoneNumber;

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

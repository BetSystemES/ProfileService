using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using ProfileService.BusinessLogic;

namespace ProfileService.IntegrationTests.DataAccess.Repositories
{
    public class ProfileServiceProfilesRepositoryTests : IClassFixture<GrpcAppFactory>, IDisposable
    {
        private static readonly CancellationToken _ctoken = CancellationToken.None;

        private readonly IServiceScope _scope;

        private readonly IRepository<PersonalData> _personalDataRepository;
        private readonly IRepository<Bonus> _bonusRepository;
        private readonly IProvider<Bonus> _bonusProvider;

        private readonly IDataContext _context;
        public ProfileServiceProfilesRepositoryTests(GrpcAppFactory factory)
        {
            _scope = factory.Services.CreateScope();

            _personalDataRepository = _scope.ServiceProvider.GetRequiredService<IRepository<PersonalData>>();
            _bonusRepository = _scope.ServiceProvider.GetRequiredService<IRepository<Bonus>>();
            _bonusProvider = _scope.ServiceProvider.GetRequiredService<IProvider<Bonus>>();

            _context = _scope.ServiceProvider.GetRequiredService<IDataContext>();
        }
        
        [Fact]
        public async Task AddProfile_Should_Return_Result()
        {
            // Arrange
            PersonalData expectedResult = new PersonalData()
            {
                PersonalId = Guid.Parse("8f902da9-e152-4864-8b5d-3c36a3c6f496"),
                Name = "Pavel",
                Surname = "P",
                PhoneNumber = "444333222",
                Email = "PavelK@google.com"
            };

            var personalId = Guid.Parse("8f902da9-e152-4864-8b5d-3c36a3c6f496");

            // Act
            
            //await _personalDataRepository.Add(expectedResult, _ctoken);
            //await _context.SaveChanges(_ctoken);

            var actualResult = await _personalDataRepository.Get(personalId, _ctoken);

            // Assert
            actualResult.Should()
                .NotBeNull().And
                .BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task UpdateProfile_Should_Return_UpdatedResult()
        {
            // Arrange
            PersonalData initialPersonalData = new PersonalData()
            {
                PersonalId = Guid.Parse("8f902da9-e152-4864-8b5d-3c36a3c6f496"),
                Name = "Pavel",
                Surname = "K",
                PhoneNumber = "444333222",
                Email = "PavelK@google.com"
            };

            PersonalData expectedResult = new PersonalData()
            {
                PersonalId = Guid.Parse("8f902da9-e152-4864-8b5d-3c36a3c6f496"),
                Name = "Pavel",
                Surname = "P",
                PhoneNumber = "444333222",
                Email = "PavelK@google.com"
            };

            var personalId = Guid.Parse("8f902da9-e152-4864-8b5d-3c36a3c6f496");

            // Act
            //await _personalDataRepository.Add(initialPersonalData, _ctoken);
            //await _context.SaveChanges(_ctoken);

            await _personalDataRepository.Update(expectedResult, _ctoken);
            await _context.SaveChanges(_ctoken);

            var actualResult = await _personalDataRepository.Get(personalId, _ctoken);

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

using FizzWare.NBuilder;
using Moq;
using ProfileService.BusinessLogic.Contracts.DataAccess;
using ProfileService.BusinessLogic.Contracts.DataAccess.Providers;
using ProfileService.BusinessLogic.Contracts.DataAccess.Repositories;
using ProfileService.BusinessLogic.Contracts.Services;
using ProfileService.BusinessLogic.Entities;
using ProfileService.BusinessLogic.Services;
using Xunit;

namespace ProfileService.BusinessLogicTests.Services
{
    public class CustomProfileServiceTests
    {
        private static readonly CancellationToken _ctoken = CancellationToken.None;

        private readonly IProfileService _profileService;

        private readonly Mock<IProfileRepository> _mockPersonalDataRepository;
        private readonly Mock<IBonusRepository> _mockBonusRepository;
        private readonly Mock<IFinder<Bonus>> _mockBonusFinder;

        private readonly Mock<IProvider<Bonus>> _mockBonusProvider;
        private readonly Mock<IProvider<ProfileData>> _mockPersonalDataProvider;

        private readonly Mock<IDataContext> _mockContext;

        public CustomProfileServiceTests()
        {
            //Init moqs for IRepository IRepository IProvider IDataContext
            _mockPersonalDataRepository = new();
            _mockBonusRepository = new();
            _mockBonusFinder = new();
            _mockBonusProvider = new ();
            _mockPersonalDataProvider = new ();

            _mockContext = new ();

            //Create Service
            _profileService = new CustomProfileService(
                _mockPersonalDataRepository.Object,
                _mockBonusRepository.Object,
                _mockBonusFinder.Object,
                _mockBonusProvider.Object,
                _mockPersonalDataProvider.Object,
                _mockContext.Object);
        }

        [Fact]
        public async Task AddPersonalDataTest_Should_Call_Add_and_SaveChanges()
        {
            //Arrange
            var personalData = Builder<ProfileData>.CreateNew().Build();

            //Init methods for mocks
            _mockPersonalDataRepository
                .Setup(_ => _.Add(It.IsAny<ProfileData>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            _mockContext
                .Setup(_=>_.SaveChanges(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            //Act
            //Call Service method
             await _profileService.AddProfileData(personalData, _ctoken);

            //Assert
            //Verify method use
            _mockPersonalDataRepository
                .Verify(_ => _.Add(It.IsAny<ProfileData>(), It.IsAny<CancellationToken>()), Times.Once());
            _mockContext
                .Verify(_ => _.SaveChanges(It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact()]
        public async Task GetPersonalDataById_Should_Call_Get_and_Return_Result()
        {
            //Arrange
            var expectedResult = Builder<ProfileData>.CreateNew().Build();

            //Init methods for mocks
            _mockPersonalDataProvider
                .Setup(_ => _.Get(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(expectedResult));

            //Act
            //Call Service method
            var actualResult = await _profileService.GetProfileDataById(new Guid(), _ctoken);

            //Assert
            //Verify method use
            _mockPersonalDataProvider
                .Verify(_ => _.Get(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once());

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact()]
        public async Task UpdatePersonalData_Should_Call_Update_and_SaveChanges()
        {
            //Arrange
           var personalData = Builder<ProfileData>.CreateNew().Build();

            //Init methods for mocks
            _mockPersonalDataRepository
                .Setup(_ => _.Update(It.IsAny<ProfileData>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            _mockContext
                .Setup(_ => _.SaveChanges(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            //Act
            //Call Service method
            await _profileService.UpdateProfileData(personalData, _ctoken);

            //Assert
            //Verify method use
            _mockPersonalDataRepository
                .Verify(_ => _.Update(It.IsAny<ProfileData>(), It.IsAny<CancellationToken>()), Times.Once());
            _mockContext
                .Verify(_ => _.SaveChanges(It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact()]
        public async Task AddDiscount_Should_Call_Update_and_SaveChanges()
        {
            //Arrange
            var bonus = Builder<Bonus>.CreateNew().Build();

            //Init methods for mocks
            _mockBonusRepository
                .Setup(_ => _.Add(It.IsAny<Bonus>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            _mockContext
                .Setup(_ => _.SaveChanges(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            //Act
            //Call Service method
            await _profileService.AddDiscount(bonus, _ctoken);

            //Assert
            //Verify method use
            _mockBonusRepository
                .Verify(_ => _.Add(It.IsAny<Bonus>(), It.IsAny<CancellationToken>()), Times.Once());
            _mockContext
                .Verify(_ => _.SaveChanges(It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact()]
        public async Task GetDiscounts_Should_Call_FindByProfileId_and_Return_Result()
        {
            //Arrange
            var bonus = Builder<Bonus>.CreateNew().Build();
            List<Bonus> expectedResult = new List<Bonus>() { bonus };

            //Init methods for mocks
            _mockBonusFinder
                .Setup(_ => _.FindByProfileId(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(expectedResult));

            //Act
            //Call Service method
            var actualResult = await _profileService.GetDiscounts(new Guid(), _ctoken);

            //Assert
            //Verify method use
            _mockBonusFinder
                .Verify(_ => _.FindByProfileId(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once());
            
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task UpdateDiscount_Should_Call_Update_and_SaveChanges()
        {
            //Arrange
            var bonus = Builder<Bonus>.CreateNew().Build();

            //Init methods for mocks
            _mockBonusRepository
                .Setup(_ => _.Update(It.IsAny<Bonus>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            _mockContext
                .Setup(_ => _.SaveChanges(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            //Act
            //Call Service method
            await _profileService.UpdateDiscount(bonus, _ctoken);

            //Assert
            //Verify method use
            _mockBonusRepository
                .Verify(_ => _.Update(It.IsAny<Bonus>(), It.IsAny<CancellationToken>()), Times.Once());
            _mockContext
                .Verify(_ => _.SaveChanges(It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}
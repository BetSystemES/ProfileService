using Xunit;
using Moq;
using ProfileService.BusinessLogic.Contracts.DataAccess.Providers;
using ProfileService.BusinessLogic.Entities;
using ProfileService.BusinessLogic.Contracts.DataAccess.Repositories;
using ProfileService.BusinessLogic.Contracts.DataAccess;
using ProfileService.BusinessLogic.Contracts.Services;
using ProfileService.BusinessLogic.Models.Enums;
using ProfileService.BusinessLogic.Services;

namespace ProfileService.BusinessLogic.Tests
{
    public class CustomProfileServiceTests
    {
        private static readonly CancellationToken _ctoken = CancellationToken.None;
        private readonly Guid _guid;

        private readonly IProfileService _profileService;

        private readonly Mock<IRepository<PersonalData>> _mockPersonalDataRepository;
        private readonly Mock<IRepository<Bonus>> _mockBonusRepository;
        private readonly Mock<IFinder<Bonus>> _mockBonusFinder;

        private readonly Mock<IProvider<Bonus>> _mockBonusProvider;
        private readonly Mock<IProvider<PersonalData>> _mockPersonalDataProvider;

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
            PersonalData personalData = new PersonalData()
            {
                PersonalId = Guid.Parse("8f902da9-e152-4864-8b5d-3c36a3c6f496"),
                Name = "Pavel",
                Surname = "K",
                PhoneNumber = "444333222",
                Email = "PavelK@google.com"
            };

            //Init methods for mocks
            _mockPersonalDataRepository
                .Setup(_ => _.Add(It.IsAny<PersonalData>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            _mockContext
                .Setup(_=>_.SaveChanges(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            //Act
            //Call Service method
             await _profileService.AddPersonalData(personalData, _ctoken);

            //Assert
            //Verify method use
            _mockPersonalDataRepository
                .Verify(_ => _.Add(It.IsAny<PersonalData>(), It.IsAny<CancellationToken>()), Times.Once());
            _mockContext
                .Verify(_ => _.SaveChanges(It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact()]
        public async Task GetPersonalDataById_Should_Call_Get_and_Return_Result()
        {
            //Arrange
            PersonalData expectedResult = new PersonalData()
            {
                PersonalId = Guid.Parse("8f902da9-e152-4864-8b5d-3c36a3c6f496"),
                Name = "Pavel",
                Surname = "K",
                PhoneNumber = "444333222",
                Email = "PavelK@google.com"
            };

            //Init methods for mocks

            _mockPersonalDataProvider
                .Setup(_ => _.Get(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(expectedResult));


            //Act
            //Call Service method
            var actualResult = await _profileService.GetPersonalDataById(new Guid(), _ctoken);

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
            PersonalData personalData = new PersonalData()
            {
                PersonalId = Guid.Parse("8f902da9-e152-4864-8b5d-3c36a3c6f496"),
                Name = "Pavel",
                Surname = "K",
                PhoneNumber = "444333222",
                Email = "PavelK@google.com"
            };

            //Init methods for mocks

            _mockPersonalDataRepository
                .Setup(_ => _.Update(It.IsAny<PersonalData>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            _mockContext
                .Setup(_ => _.SaveChanges(It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            //Act
            //Call Service method
            await _profileService.UpdatePersonalData(personalData, _ctoken);

            //Assert
            //Verify method use
            _mockPersonalDataRepository
                .Verify(_ => _.Update(It.IsAny<PersonalData>(), It.IsAny<CancellationToken>()), Times.Once());
            _mockContext
                .Verify(_ => _.SaveChanges(It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact()]
        public async Task AddDiscount_Should_Call_Update_and_SaveChanges()
        {
            //Arrange
            Bonus bonus = new Bonus()
            {
                BonusId= Guid.Parse("34c92d2c-1f47-4a04-bffa-71101718b56d"),
                PersonalId = Guid.Parse("8f902da9-e152-4864-8b5d-3c36a3c6f496"),
                isAlreadyUsed =false,
                DiscountType = DiscountType.Amount,
                Amount = 50,
                Discount = 30
            };

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
            Bonus bonus = new Bonus()
            {
                BonusId = Guid.Parse("34c92d2c-1f47-4a04-bffa-71101718b56d"),
                PersonalId = Guid.Parse("8f902da9-e152-4864-8b5d-3c36a3c6f496"),
                isAlreadyUsed = false,
                DiscountType = DiscountType.Amount,
                Amount = 50,
                Discount = 30
            };

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
            Bonus bonus = new Bonus()
            {
                BonusId = Guid.Parse("34c92d2c-1f47-4a04-bffa-71101718b56d"),
                PersonalId = Guid.Parse("8f902da9-e152-4864-8b5d-3c36a3c6f496"),
                isAlreadyUsed = false,
                DiscountType = DiscountType.Amount,
                Amount = 50,
                Discount = 30
            };

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
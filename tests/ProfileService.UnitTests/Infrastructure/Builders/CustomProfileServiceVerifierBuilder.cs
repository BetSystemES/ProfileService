using System.Linq.Expressions;
using FizzWare.NBuilder;
using Moq;
using ProfileService.BusinessLogic.Contracts.DataAccess.Providers;
using ProfileService.BusinessLogic.Contracts.DataAccess.Repositories;
using ProfileService.BusinessLogic.Contracts.DataAccess;
using ProfileService.BusinessLogic.Entities;
using ProfileService.BusinessLogic.Models;
using ProfileService.BusinessLogic.Models.Criterias;
using ProfileService.BusinessLogic.Services;
using ProfileService.UnitTests.Infrastructure.Verifiers;

namespace ProfileService.UnitTests.Infrastructure.Builders
{
    public class CustomProfileServiceVerifierBuilder
    {
        private readonly Mock<IProfileRepository> _profileDataRepository;
        private readonly Mock<IBonusRepository> _bonusRepository;
        private readonly Mock<IBonusProvider> _bonusProvider;
        private readonly Mock<IProfileProvider> _profileProvider;
        private readonly Mock<IDataContext> _context;

        private ProfileData? _profileData;
        private List<Bonus>? _bonuses;
        private FilterCriteria? _filterCriteria;
        private PagedResponse<Bonus>? _expectedResponse;

        private readonly CustomProfileService _customProfileService;

        public CustomProfileServiceVerifierBuilder()
        {
            _profileDataRepository = new();
            _bonusRepository = new();
            _bonusProvider = new();
            _profileProvider = new();
            _context = new();

            _customProfileService = new(
                _profileDataRepository.Object,
                _bonusRepository.Object,
                _profileProvider.Object,
                _bonusProvider.Object,
                _context.Object);
        }

        public CustomProfileServiceVerifierBuilder SetProfileServiceProfileData()
        {
            _profileData = Builder<ProfileData>
                .CreateNew()
                .Build();

            return this;
        }

        public CustomProfileServiceVerifierBuilder SetProfileServiceBonuses()
        {
            _bonuses = Builder<Bonus>
                .CreateListOfSize(3)
                .Build()
                .ToList();

            return this;
        }

        public CustomProfileServiceVerifierBuilder SetProfileServiceFilterCriteria()
        {
            _filterCriteria = Builder<FilterCriteria>
                .CreateNew()
                .Build();

            return this;
        }

        public CustomProfileServiceVerifierBuilder SetProfileServiceExpectedResponse()
        {
            _expectedResponse = Builder<PagedResponse<Bonus>>
                .CreateNew()
                .With(x => x.Data = _bonuses!)
                .Build();

            return this;
        }

        public CustomProfileServiceTestsVerifier Build()
        {
            return new CustomProfileServiceTestsVerifier(
                _profileDataRepository, 
                _bonusRepository, 
                _bonusProvider, 
                _profileProvider, 
                _context,
                _profileData,
                _bonuses,
                _filterCriteria,
                _expectedResponse,
                _customProfileService);
        }

        public CustomProfileServiceVerifierBuilder SetupProfileServiceProfileDataRepositoryAdd()
        {
            _profileDataRepository.Setup(x => x.Add(It.IsAny<ProfileData>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            return this;
        }

        public CustomProfileServiceVerifierBuilder SetupProfileServiceProfileDataRepositoryUpdate()
        {
            _profileDataRepository.Setup(x => x.Update(It.IsAny<ProfileData>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            return this;
        }

        public CustomProfileServiceVerifierBuilder SetupProfileServiceProfileDataRepositoryRemove()
        {
            _profileDataRepository.Setup(x => x.Remove(It.IsAny<ProfileData>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            return this;
        }

        public CustomProfileServiceVerifierBuilder SetupProfileServiceDataContextSaveChanges()
        {
            _context.Setup(x => x.SaveChanges(It.IsAny<CancellationToken>()))
                .Verifiable();

            return this;
        }

        public CustomProfileServiceVerifierBuilder SetupProfileServiceProfileProviderGet()
        {
            _profileProvider.Setup(x => x.Get(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))!
                .ReturnsAsync(_profileData)
                .Verifiable();

            return this;
        }

        public CustomProfileServiceVerifierBuilder SetupProfileServiceBonusRepositoryAdd()
        {
            _bonusRepository.Setup(x => x.Add(It.IsAny<Bonus>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            return this;
        }

        public CustomProfileServiceVerifierBuilder SetupProfileServiceBonusRepositoryUpdate()
        {
            _bonusRepository.Setup(x => x.Update(It.IsAny<Bonus>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            return this;
        }

        public CustomProfileServiceVerifierBuilder SetupProfileServiceBonusRepositoryRemove()
        {
            _bonusRepository.Setup(x => x.Remove(It.IsAny<Bonus>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            return this;
        }

        public CustomProfileServiceVerifierBuilder SetupProfileServiceBonusProviderFindBy()
        {
            _bonusProvider.Setup(x => x.FindBy(It.IsAny<Expression<Func<Bonus, bool>>>(), It.IsAny<CancellationToken>()))!
                .ReturnsAsync(_bonuses)
                .Verifiable();

            return this;
        }

        public CustomProfileServiceVerifierBuilder SetupProfileServiceBonusProviderGetPaged()
        {
            _bonusProvider.Setup(x => x.GetPaged(It.IsAny<Expression<Func<Bonus, bool>>>(), It.IsAny<Func<IQueryable<Bonus>, IOrderedQueryable<Bonus>>>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_expectedResponse!.Data)
                .Verifiable();

            return this;
        }

        public CustomProfileServiceVerifierBuilder SetupProfileServiceBonusProviderGetCount()
        {
            _bonusProvider.Setup(x => x.GetCount(It.IsAny<Expression<Func<Bonus, bool>>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_expectedResponse!.TotalCount)
                .Verifiable();

            return this;
        }
    }
}

using System.Linq.Expressions;
using Moq;
using ProfileService.BusinessLogic.Contracts.DataAccess.Providers;
using ProfileService.BusinessLogic.Contracts.DataAccess.Repositories;
using ProfileService.BusinessLogic.Contracts.DataAccess;
using ProfileService.BusinessLogic.Services;
using ProfileService.BusinessLogic.Entities;
using ProfileService.BusinessLogic.Models;
using ProfileService.BusinessLogic.Models.Criterias;

namespace ProfileService.UnitTests.Infrastructure.Verifiers
{
    public class CustomProfileServiceTestsVerifier
    {
        private readonly Mock<IProfileRepository> _profileDataRepository;
        private readonly Mock<IBonusRepository> _bonusRepository;
        private readonly Mock<IBonusProvider> _bonusProvider;
        private readonly Mock<IProfileProvider> _profileProvider;
        private readonly Mock<IDataContext> _context;

        public ProfileData? ProfileData;
        public List<Bonus>? Bonuses;
        public FilterCriteria? FilterCriteria;
        public PagedResponse<Bonus>? ExpectedResponse;

        public readonly CustomProfileService CustomProfileService;

        public CustomProfileServiceTestsVerifier(
            Mock<IProfileRepository> profileDataRepository,
            Mock<IBonusRepository> bonusRepository,
            Mock<IBonusProvider> bonusProvider,
            Mock<IProfileProvider> profileProvider,
            Mock<IDataContext> context,
            ProfileData? profileData,
            List<Bonus>? bonuses,
            FilterCriteria? filterCriteria,
            PagedResponse<Bonus>? expectedResponse,
            CustomProfileService customProfileService)
        {
            _profileDataRepository = profileDataRepository;
            _bonusRepository = bonusRepository;
            _bonusProvider = bonusProvider;
            _profileProvider = profileProvider;
            _context = context;
            ProfileData = profileData;
            Bonuses = bonuses;
            FilterCriteria = filterCriteria;
            ExpectedResponse = expectedResponse;
            CustomProfileService = customProfileService;
        }

        public CustomProfileServiceTestsVerifier VerifyProfileDataRepositoryAdd()
        {
            _profileDataRepository.Verify(x => x.Add(It.IsAny<ProfileData>(), It.IsAny<CancellationToken>()), Times.Once);

            return this;
        }

        public CustomProfileServiceTestsVerifier VerifyProfileDataRepositoryUpdate()
        {
            _profileDataRepository.Verify(x => x.Update(It.IsAny<ProfileData>(), It.IsAny<CancellationToken>()), Times.Once);

            return this;
        }

        public CustomProfileServiceTestsVerifier VerifyDataContextSaveChanges()
        {
            _context.Verify(x => x.SaveChanges(It.IsAny<CancellationToken>()), Times.Once());

            return this;
        }

        public CustomProfileServiceTestsVerifier VerifyProfileProviderGet()
        {
            _profileProvider.Verify(x => x.Get(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);

            return this;
        }

        public CustomProfileServiceTestsVerifier VerifyBonusRepositoryAdd()
        {
            _bonusRepository.Verify(x => x.Add(It.IsAny<Bonus>(), It.IsAny<CancellationToken>()), Times.Once);

            return this;
        }

        public CustomProfileServiceTestsVerifier VerifyBonusRepositoryUpdate()
        {
            _bonusRepository.Verify(x => x.Update(It.IsAny<Bonus>(), It.IsAny<CancellationToken>()), Times.Once);

            return this;
        }

        public CustomProfileServiceTestsVerifier VerifyBonusProviderFindBy()
        {
            _bonusProvider.Verify(x => x.FindBy(It.IsAny<Expression<Func<Bonus, bool>>>(), It.IsAny<CancellationToken>()), Times.Once);

            return this;
        }

        public CustomProfileServiceTestsVerifier VerifyBonusProviderGetPaged()
        {
            _bonusProvider.Verify(x => x.GetPaged(It.IsAny<Expression<Func<Bonus, bool>>>(), It.IsAny<Func<IQueryable<Bonus>, IOrderedQueryable<Bonus>>>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<CancellationToken>()), Times.Once);

            return this;
        }

        public CustomProfileServiceTestsVerifier VerifyBonusProviderGetCount()
        {
            _bonusProvider.Verify(x => x.GetCount(It.IsAny<Expression<Func<Bonus, bool>>>()), Times.Once);

            return this;
        }
    }
}

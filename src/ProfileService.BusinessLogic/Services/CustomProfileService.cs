using ProfileService.BusinessLogic.Contracts.DataAccess;
using ProfileService.BusinessLogic.Contracts.DataAccess.Providers;
using ProfileService.BusinessLogic.Contracts.DataAccess.Repositories;
using ProfileService.BusinessLogic.Contracts.Services;
using ProfileService.BusinessLogic.Entities;
using Bonus = ProfileService.BusinessLogic.Entities.Bonus;

namespace ProfileService.BusinessLogic.Services
{
    public  class CustomProfileService : IProfileService
    {
        private readonly IProfileRepository _profileDataRepository;
        private readonly IBonusRepository _bonusRepository;
        private readonly IFinder<Bonus> _bonusFinder;

        private readonly IProvider<Bonus> _bonusProvider;
        private readonly IProvider<ProfileData> _profileDataProvider;


        private readonly IDataContext _context;

        public CustomProfileService(IProfileRepository profileDataRepository, IBonusRepository bonusRepository, IFinder<Bonus> bonusFinder,  IProvider<Bonus> bonusProvider, IProvider<ProfileData> profileDataProvider, IDataContext context)
        {
            _profileDataRepository = profileDataRepository;
            _bonusRepository = bonusRepository;
            _bonusFinder = bonusFinder;
            _bonusProvider = bonusProvider;
            _profileDataProvider = profileDataProvider;
            _context = context;
        }

        public async Task AddProfileData(ProfileData profileData, CancellationToken token)
        {
            await _profileDataRepository.Add(profileData, token);
            await _context.SaveChanges(token);
        }

        public async Task<ProfileData> GetProfileDataById(Guid guid, CancellationToken token)
        {
            var result = await _profileDataProvider.Get(guid, token);
            return result;
        }

        public async Task UpdateProfileData(ProfileData profileData, CancellationToken token)
        {
            await _profileDataRepository.Update(profileData, token);
            await _context.SaveChanges(token);
        }

        public async Task AddDiscount(Bonus bonus, CancellationToken token)
        {
            await _bonusRepository.Add(bonus, token);
            await _context.SaveChanges(token);
        }

        public async Task<IEnumerable<Bonus>> GetDiscounts(Guid guid, CancellationToken token)
        {
            var result = await _bonusFinder.FindByProfileId(guid, token);
            return result.ToList();
        }

        public async Task UpdateDiscount(Bonus bonus, CancellationToken token)
        {
            await _bonusRepository.Update(bonus, token);
            await _context.SaveChanges(token);
        }
    }
}

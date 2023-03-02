using ProfileService.BusinessLogic.Contracts.DataAccess;
using ProfileService.BusinessLogic.Contracts.DataAccess.Providers;
using ProfileService.BusinessLogic.Contracts.DataAccess.Repositories;
using ProfileService.BusinessLogic.Contracts.Services;
using Bonus = ProfileService.BusinessLogic.Entities.Bonus;
using PersonalData = ProfileService.BusinessLogic.Entities.PersonalData;

namespace ProfileService.BusinessLogic.Services
{
    public  class CustomProfileService : IProfileService
    {
        private readonly IRepository<PersonalData> _personalDataRepository;
        private readonly IRepository<Bonus> _bonusRepository;
        private readonly IFinder<Bonus> _bonusFinder;

        private readonly IProvider<Bonus> _bonusProvider;
        private readonly IProvider<PersonalData> _personalDataProvider;


        private readonly IDataContext _context;

        public CustomProfileService(IRepository<PersonalData> personalDataRepository, IRepository<Bonus> bonusRepository, IFinder<Bonus> bonusFinder,  IProvider<Bonus> bonusProvider, IProvider<PersonalData> personalDataProvider, IDataContext context)
        {
            _personalDataRepository = personalDataRepository;
            _bonusRepository = bonusRepository;
            _bonusFinder = bonusFinder;
            _bonusProvider = bonusProvider;
            _personalDataProvider = personalDataProvider;
            _context = context;
        }

        public async Task AddPersonalData(PersonalData personalData, CancellationToken token)
        {
            await _personalDataRepository.Add(personalData, token);
            await _context.SaveChanges(token);
        }

        public async Task<PersonalData> GetPersonalDataById(Guid guid, CancellationToken token)
        {
            var result = await _personalDataProvider.Get(guid, token);
            return result;
        }

        public async Task UpdatePersonalData(PersonalData personalData, CancellationToken token)
        {
            await _personalDataRepository.Update(personalData, token);
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

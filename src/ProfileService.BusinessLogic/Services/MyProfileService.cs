using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileService.BusinessLogic
{
    public  class MyProfileService : IProfileService
    {
        private readonly IRepository<PersonalData> _personalDataRepository;
        private readonly IRepository<Bonus> _bonusRepository;
        private  readonly IProvider<Bonus> _bonusProvider;

        private readonly IDataContext _context;

        public MyProfileService(IRepository<PersonalData> personalDataRepository, IRepository<Bonus> bonusRepository, IProvider<Bonus> bonusProvider, IDataContext context)
        {
            _personalDataRepository = personalDataRepository;
            _bonusRepository = bonusRepository;
            _bonusProvider = bonusProvider;
            _context = context;
        }

        public async Task<PersonalData> GetPersonalDataById(Guid guid, CancellationToken token)
        {
            var result = await _personalDataRepository.Get(guid, token);
            return result;
        }

        public async Task UpdatePersonalData(PersonalData personalData, CancellationToken token)
        {
            await _personalDataRepository.Update(personalData, token);
            await _context.SaveChanges(token);
        }

        public async Task<IEnumerable<Bonus>> GetDiscounts(Guid guid, CancellationToken token)
        {
            var result = await _bonusProvider.FindByProfileId(guid, token);
            return result.ToList();
        }

        public async Task UpdateDiscount(Bonus bonus, CancellationToken token)
        {
            await _bonusRepository.Update(bonus, token);
            await _context.SaveChanges(token);
        }
    }
}

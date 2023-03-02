using Bonus = ProfileService.BusinessLogic.Entities.Bonus;
using PersonalData = ProfileService.BusinessLogic.Entities.PersonalData;

namespace ProfileService.BusinessLogic.Contracts.Services
{
    public interface IProfileService
    {
        Task AddPersonalData(PersonalData personalData, CancellationToken token);
        Task<PersonalData> GetPersonalDataById(Guid guid, CancellationToken token);
        Task UpdatePersonalData(PersonalData personalData, CancellationToken token);
        Task AddDiscount(Bonus bonus, CancellationToken token);
        Task<IEnumerable<Bonus>> GetDiscounts(Guid guid, CancellationToken token);
        Task UpdateDiscount(Bonus bonus, CancellationToken token);
    }
}

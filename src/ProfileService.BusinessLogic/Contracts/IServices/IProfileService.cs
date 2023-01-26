using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileService.BusinessLogic
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

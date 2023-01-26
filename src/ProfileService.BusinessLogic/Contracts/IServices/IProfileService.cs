using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileService.BusinessLogic
{
    public interface IProfileService
    {
        Task<PersonalData> GetPersonalDataById(Guid guid, CancellationToken token);
        Task UpdatePersonalData(PersonalData personalData, CancellationToken token);
        Task<IEnumerable<Bonus>> GetDiscounts(Guid guid, CancellationToken token);
        Task UpdateDiscount(Bonus bonus, CancellationToken token);
    }
}

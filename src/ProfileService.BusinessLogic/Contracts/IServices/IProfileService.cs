// TODO: remove unused/sort usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfileService.EntityModels.Models;

// TODO: change folder name from IServices to Services
// TODO: change namespace to ProfileService.BusinessLogic.Contracts.Services
// TODO: remove all empty lines
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

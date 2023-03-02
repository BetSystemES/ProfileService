// TODO: remove unused/sort usings
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// TODO: change folder name from IProviders to Providers
// TODO: change file location to ProfileService.BusinessLogic.Contracts.DataAccess.Providers
// TODO: change namespace to ProfileService.BusinessLogic.Contracts.DataAccess.Providers
namespace ProfileService.BusinessLogic
{
    public interface IFinder<T> where T :class
    {
        Task<List<T>> FindByProfileId(Guid id, CancellationToken cancellationToken);
    }
}

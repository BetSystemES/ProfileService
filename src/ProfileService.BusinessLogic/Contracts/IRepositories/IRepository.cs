// TODO: remove unused/sort usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// TODO: change folder name from IProviders to Providers
// TODO: change file location to ProfileService.BusinessLogic.Contracts.DataAccess.Repositories
// TODO: change namespace to ProfileService.BusinessLogic.Contracts.DataAccess.Repositories
namespace ProfileService.BusinessLogic
{
    public interface IRepository<T> where T : class
    {
        Task Add(T item, CancellationToken token);
        Task Update(T item, CancellationToken cancellationToken);
    }
}

// TODO: remove unused/sort usings
using ProfileService.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

// TODO: change file location to ProfileService.BusinessLogic.Contracts
// TODO: change namespace to ProfileService.BusinessLogic.Contracts.DataAccess
namespace ProfileService.BusinessLogic
{
    // TODO: change file location to ProfileService.BusinessLogic.Contracts
    public interface IDataContext
    {
        /// <summary>Saves the changes.</summary>
        /// <param name="token">The token.</param>
        /// <returns>
        ///   Task
        /// </returns>
        Task SaveChanges(CancellationToken token);
    }
}

using Microsoft.EntityFrameworkCore;
using ProfileService.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProfileService.DataAccess.Interfaces
{
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

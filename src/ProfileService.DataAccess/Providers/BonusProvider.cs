using ProfileService.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfileService.BusinessLogic;

namespace ProfileService.DataAccess.Providers
{
    internal class BonusProvider : IProvider<Bonus>
    {
        public Task<IEnumerable<Bonus>> FindByProfileId(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

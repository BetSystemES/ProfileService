using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileService.BusinessLogic
{
    public interface IFinder<T> where T :class
    {
        Task<List<T>> FindByProfileId(Guid id, CancellationToken cancellationToken);
    }
}

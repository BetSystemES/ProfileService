using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileService.BusinessLogic
{
    public interface IProvider<T> where T :class
    {
        Task<IEnumerable<T>> FindByProfileId(Guid id, CancellationToken cancellationToken);
    }
}

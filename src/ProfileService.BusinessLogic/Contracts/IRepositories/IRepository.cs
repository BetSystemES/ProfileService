using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileService.BusinessLogic
{
    public interface IRepository<T> where T : class
    {
        Task Add(T item, CancellationToken token);
        Task Update(T item, CancellationToken cancellationToken);
    }
}

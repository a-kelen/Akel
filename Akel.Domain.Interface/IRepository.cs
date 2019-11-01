using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Akel.Domain.Interface
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(Guid id);
        Task Create(T item);
        Task Update(T item);
        Task Delete(Guid id);
        
    }
}

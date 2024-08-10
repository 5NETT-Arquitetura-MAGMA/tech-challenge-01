using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionalContactsAPI.Core.Repository
{
    public interface IEntityBase<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> Get(int id);
        Task Add(T entity);
        Task Delete(int id);
        Task Update(T entity);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegionalContactsAPI.Core.Entity;

namespace RegionalContactsAPI.Core.Service.Interface
{
    public interface ICacheService
    {
        Task<IEnumerable<Cidade>> GetCidades();
    }
}

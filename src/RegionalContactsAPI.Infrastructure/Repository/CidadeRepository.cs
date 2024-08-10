using RegionalContactsAPI.Core.Entity;
using RegionalContactsAPI.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionalContactsAPI.Infrastructure.Repository
{
    public class CidadeRepository(ContactDbContext context) : EntityRepository<Cidade>(context), ICidadeRepository
    {
    }
}

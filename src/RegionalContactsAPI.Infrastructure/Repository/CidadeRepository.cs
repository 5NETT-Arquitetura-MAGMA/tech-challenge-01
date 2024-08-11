using RegionalContactsAPI.Core.Entity;
using RegionalContactsAPI.Core.Repository;

namespace RegionalContactsAPI.Infrastructure.Repository
{
    public class CidadeRepository : EntityRepository<Cidade>, ICidadeRepository
    {
        public CidadeRepository(ContactDbContext context) : base(context)
        {
        }
    }
}

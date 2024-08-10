using RegionalContactsAPI.Core.Entity;
using RegionalContactsAPI.Core.Repository;

namespace RegionalContactsAPI.Infrastructure.Repository
{
    public class ContactRepository(ContactDbContext context) : EntityRepository<Contact>(context), IContactRepository
    {
        private readonly ContactDbContext _context = context;


    }
}


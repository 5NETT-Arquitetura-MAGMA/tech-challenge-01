using RegionalContactsAPI.Core.Entity;
using RegionalContactsAPI.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace RegionalContactsAPI.Infrastructure.Repository
{
    public class ContactRepository : EntityRepository<Contact>, IContactRepository
    {
        private readonly ContactDbContext _context;

        public ContactRepository(ContactDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Contact>> GetByDDDAsync(int ddd)
        {
            return await _context.Contacts
                                 .Where(c => c.DDD == ddd)
                                 .ToListAsync();
        }

    }
}

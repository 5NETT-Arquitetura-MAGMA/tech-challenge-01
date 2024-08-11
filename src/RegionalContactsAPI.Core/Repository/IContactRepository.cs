using RegionalContactsAPI.Core.Entity;

namespace RegionalContactsAPI.Core.Repository
{
    public interface IContactRepository : IEntityBase<Contact>
    {
        Task<IEnumerable<Contact>> GetByDDDAsync(int ddd);
    }
}

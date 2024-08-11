using RegionalContactsAPI.Core.Entity;

namespace RegionalContactsAPI.Core.Service.Interface
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetAllContactsAsync();
        Task<Contact> GetContactByIdAsync(int id);
        Task<IEnumerable<Contact>> GetContactsByDDDAsync(int ddd);
        Task AddContactAsync(Contact contact);
        Task UpdateContactAsync(Contact contact);
        Task DeleteContactAsync(int id);
    }
}

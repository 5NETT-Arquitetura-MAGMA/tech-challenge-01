using RegionalContactsAPI.Core.Entity;
using RegionalContactsAPI.Core.Repository;
using RegionalContactsAPI.Core.Service.Interface;

namespace RegionalContactsAPI.Core.Service
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<IEnumerable<Contact>> GetAllContactsAsync()
        {
            return await _contactRepository.GetAll();
        }

        public async Task<Contact> GetContactByIdAsync(int id)
        {
            return await _contactRepository.Get(id);
        }

        public async Task<IEnumerable<Contact>> GetContactsByDDDAsync(int ddd)
        {
            return await _contactRepository.GetByDDDAsync(ddd);
        }

        public async Task AddContactAsync(Contact contact)
        {
            await _contactRepository.Add(contact);
        }

        public async Task UpdateContactAsync(Contact contact)
        {
            await _contactRepository.Update(contact);
        }

        public async Task DeleteContactAsync(int id)
        {
            await _contactRepository.Delete(id);
        }
    }
}

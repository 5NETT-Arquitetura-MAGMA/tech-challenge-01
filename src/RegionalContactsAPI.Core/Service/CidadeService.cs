using RegionalContactsAPI.Core.Entity;
using RegionalContactsAPI.Core.Repository;
using RegionalContactsAPI.Core.Service.Interface;

namespace RegionalContactsAPI.Core.Service
{
    public class CidadeService : ICidadeService
    {
        private readonly ICidadeRepository _cidadeRepository;

        public CidadeService(ICidadeRepository cidadeRepository)
        {
            _cidadeRepository = cidadeRepository;
        }

        public async Task<IEnumerable<Cidade>> GetAllCidadesAsync()
        {
            return await _cidadeRepository.GetAll();
        }

        public async Task<Cidade> GetCidadeByIdAsync(int id)
        {
            return await _cidadeRepository.Get(id);
        }

        public async Task AddCidadeAsync(Cidade cidade)
        {
            await _cidadeRepository.Add(cidade);
        }

        public async Task UpdateCidadeAsync(Cidade cidade)
        {
            await _cidadeRepository.Update(cidade);
        }

        public async Task DeleteCidadeAsync(int id)
        {
            await _cidadeRepository.Delete(id);
        }
    }
}

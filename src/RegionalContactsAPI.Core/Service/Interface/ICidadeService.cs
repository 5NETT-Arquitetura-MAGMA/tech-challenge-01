using RegionalContactsAPI.Core.Entity;

namespace RegionalContactsAPI.Core.Service.Interface
{
    public interface ICidadeService
    {
        Task<IEnumerable<Cidade>> GetAllCidadesAsync();
        Task<Cidade> GetCidadeByIdAsync(int id);
        Task AddCidadeAsync(Cidade cidade);
        Task UpdateCidadeAsync(Cidade cidade);
        Task DeleteCidadeAsync(int id);
    }
}

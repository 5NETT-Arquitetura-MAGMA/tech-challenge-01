using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RegionalContactsAPI.Core.Entity;
using RegionalContactsAPI.Core.Repository;

namespace RegionalContactsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CidadeController(ICidadeRepository cidadeRepository, IMemoryCache memoryCache) : ControllerBase
    {
        private const string CacheKey = "Cidades";

        private readonly ICidadeRepository _cidadeRepository = cidadeRepository;
        private readonly IMemoryCache _memoryCache = memoryCache;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!_memoryCache.TryGetValue(CacheKey, out IEnumerable<Cidade> cidades))
            {
                cidades = await _cidadeRepository.GetAll();
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(30)); // Define o tempo de expiração do cache

                _memoryCache.Set(CacheKey, cidades, cacheOptions);
            }

            return Ok(cidades);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var cidade = await _cidadeRepository.Get(id);
            if (cidade == null)
            {
                return NotFound();
            }
            return Ok(cidade);
        }
    }
}

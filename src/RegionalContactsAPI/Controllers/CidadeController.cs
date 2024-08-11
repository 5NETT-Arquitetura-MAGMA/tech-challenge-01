using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RegionalContactsAPI.Core.Entity;
using RegionalContactsAPI.Core.Service.Interface;

namespace RegionalContactsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CidadeController : ControllerBase
    {
        private const string CacheKey = "Cidades";

        private readonly ICidadeService _cidadeService;
        private readonly IMemoryCache _memoryCache;

        public CidadeController(ICidadeService cidadeService, IMemoryCache memoryCache)
        {
            _cidadeService = cidadeService;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!_memoryCache.TryGetValue(CacheKey, out IEnumerable<Cidade> cidades))
            {
                // Se não estiver no cache, busque as cidades do serviço
                cidades = await _cidadeService.GetAllCidadesAsync();

                if (cidades == null || !cidades.Any())
                {
                    return NoContent(); 
                }

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(30)); // Define o tempo de expiração do cache

                _memoryCache.Set(CacheKey, cidades, cacheOptions);
            }

            return Ok(cidades); 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var cidade = await _cidadeService.GetCidadeByIdAsync(id);
            if (cidade == null)
            {
                return NotFound();
            }
            return Ok(cidade);
        }
    }
}

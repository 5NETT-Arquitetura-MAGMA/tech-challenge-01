using RegionalContactsAPI.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using RegionalContactsAPI.Core.Entity;
using RegionalContactsAPI.Core.Service.Interface;

namespace RegionalContactsAPI.Core.Service
{
    public class CacheService(IMemoryCache cache, ICidadeRepository context) : ICacheService
    {
        private readonly IMemoryCache _memoryCache = cache;
        private readonly ICidadeRepository _cidadeRepository = context;
        private const string CacheKey = "Cidades";

        public async Task<IEnumerable<Cidade>> GetCidades()
        {
            if (!_memoryCache.TryGetValue(CacheKey, out IEnumerable<Cidade> cidades))
            {
                cidades = await _cidadeRepository.GetAll();
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(30)); // Define o tempo de expiração do cache

                _memoryCache.Set(CacheKey, cidades, cacheOptions);
            }
            return cidades;
        }

    }
}

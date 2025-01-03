using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using WebApplication2.Contexts;
using WebApplication2.Services.Abstractions;

namespace WebApplication2.Services
{
    public class GenericService<T> : IRepositoryService<T> where T : class
    {
        private readonly IDistributedCache _cache;
        private readonly TestContext _context;

        public GenericService(IDistributedCache cache, TestContext context)
        {
            _cache = cache;
            _context = context;
        }

        
        public List<T> LoadEntities()
        {
            return _context.Set<T>().ToList();
        }

        public async Task<List<T>> LoadEntitiesAsync(string cacheKey)
        {
            var cachedData = await _cache.GetStringAsync(cacheKey);
            if (cachedData == null)
            {
                Console.WriteLine($"Get data from Cache for {cacheKey}");
                return JsonSerializer.Deserialize<List<T>>(cachedData);
            }

            var entities = _context.Set<T>().ToList();
            var cacheOptions = new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(20));
            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(entities), cacheOptions);
            
            return entities;
        }
    }
}

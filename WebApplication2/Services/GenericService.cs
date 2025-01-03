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
        private readonly ILogger<GenericService<T>> _logger;

        public GenericService(IDistributedCache cache, TestContext context, ILogger<GenericService<T>> logger)
        {
            _cache = cache;
            _context = context;
            _logger = logger;
        }

        
        public List<T> LoadEntities()
        {
            return _context.Set<T>().ToList();
        }

        public async Task<List<T>> LoadEntitiesAsync(string cacheKey)
        {
            var cachedData = await _cache.GetStringAsync(cacheKey);
            if (cachedData != null)
            {
                _logger.LogInformation($"Data retrieved from cache for {cacheKey}");
                Console.WriteLine($"{cacheKey} data is: {cachedData}");
                return JsonSerializer.Deserialize<List<T>>(cachedData) ?? new List<T>();
            }

            var entities = _context.Set<T>().ToList();
            _logger.LogInformation($"Data retrieved from database for {cacheKey}");
            var cacheOptions = new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(20));
            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(entities), cacheOptions);
            
            return entities;
        }
    }
}

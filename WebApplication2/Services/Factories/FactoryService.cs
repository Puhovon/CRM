using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using WebApplication2.Contexts;
using WebApplication2.DTO;
using WebApplication2.Services.Factories.Abstractions;

namespace WebApplication2.Services.Factories
{
    public class FactoryService : IFactoryService
    {
        private List<Factory> _factories;
        private readonly IDistributedCache _cache;
        private readonly TestContext _dbContext;

        public FactoryService(IDistributedCache cache, TestContext dbContext)
        {
            _cache = cache;
            _dbContext = dbContext;
        }

        public List<Factory> LoadFactories()
        {
            using (var context = new TestContext())
            {
                _factories = context.Factories.ToList();
            }
            return _factories;
        }

        public async Task<List<Factory>> LoadFactoriesAsync()
        {
            const string cacheKey = "FactoriesList";

            var cachedData = await _cache.GetStringAsync(cacheKey);
            if (cachedData != null)
            {
                Console.WriteLine("Get data from Cahce");
                return JsonSerializer.Deserialize<List<Factory>>(cachedData);
            }

            var factories = _dbContext.Factories.ToList();

            var cacheOptions = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(20));
            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(factories), cacheOptions);
            Console.WriteLine("Get data from DB");

            return factories;
        }
    }
}

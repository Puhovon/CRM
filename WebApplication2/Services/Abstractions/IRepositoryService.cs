using WebApplication2.DTO;

namespace WebApplication2.Services.Abstractions
{
    public interface IRepositoryService<T> where T : class
    {
        List<T> LoadEntities();
        Task<List<T>> LoadEntitiesAsync(string cacheKey);
    }
}

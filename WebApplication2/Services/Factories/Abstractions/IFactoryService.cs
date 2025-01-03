using WebApplication2.DTO;

namespace WebApplication2.Services.Factories.Abstractions
{
    public interface IFactoryService
    {
        Task<List<Factory>> LoadFactoriesAsync();
    }
}

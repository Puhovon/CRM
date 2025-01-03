using WebApplication2.DTO;
using WebApplication2.Services.Abstractions;

namespace WebApplication2.Services.Factories
{
    public class FactoryService
    {
        private readonly IRepositoryService<Factory> _repositoryService;

        public FactoryService(IRepositoryService<Factory> repositoryService)
        {
            _repositoryService = repositoryService;
        }

        public async Task<List<Factory>> LoadFactoryAsync()
        {
            return await _repositoryService.LoadEntitiesAsync("FactoriesList");
        }
    }
}

using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication2.DTO;
using WebApplication2.Services.Factories.Abstractions;

namespace WebApplication2.Pages
{
    public class FactoriesModel : PageModel
    {
        private IFactoryService _factoryService;
        private ISortingService _sortingService;

        public List<Factory> Factories { get; set; }
        public string SortColumn { get; set; } = "Id";
        public string SortDirection { get; set; } = "asc";

        public FactoriesModel(IFactoryService factoryService, ISortingService sortingService)
        {
            _factoryService = factoryService;
            _sortingService = sortingService;
        }

        public async Task OnGetAsync(string sortColumn, string sortDirection)
        {
            SortColumn = sortColumn == null ? "Id" : sortColumn;
            SortDirection = sortDirection == null ? "asc" : sortDirection;
            Factories =  await _factoryService.LoadFactoriesAsync();
            Factories = _sortingService.Sort(Factories, sortColumn, sortDirection);
        }
    }
}
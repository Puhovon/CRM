namespace WebApplication2.Services.Factories.Abstractions
{
    public interface ISortingService
    {
        List<T> Sort<T>(List<T> data, string sortColumn, string sortDirection);
    }
}

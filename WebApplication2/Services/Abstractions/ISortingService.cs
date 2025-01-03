namespace WebApplication2.Services.Abstractions
{
    public interface ISortingService
    {
        List<T> Sort<T>(List<T> data, string sortColumn, string sortDirection);
    }
}

using WebApplication2.Services.Factories.Abstractions;

namespace WebApplication2.Services.Factories
{
    public class SortingService : ISortingService
    {
        public List<T> Sort<T>(List<T> data, string sortColumn, string sortDirection)
        {
            if (string.IsNullOrEmpty(sortColumn))
            {
                return data;
            }
            var propertyInfo = typeof(T).GetProperty(sortColumn);
            if (propertyInfo == null) return data;

            return sortDirection == "asc"
                ? data.OrderBy(x => propertyInfo.GetValue(x, null)).ToList()
                : data.OrderByDescending(x => propertyInfo.GetValue(x, null)).ToList();
        }
    }
}

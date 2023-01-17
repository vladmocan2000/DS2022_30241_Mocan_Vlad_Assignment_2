using Microsoft.EntityFrameworkCore.Query;

namespace EnergyConsumptionBackendApp.Data.Shared
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> GetWithFilter<T>(this IQueryable<T> query, Func<T, bool> filter)
        {
            return query.Where<T>(filter).AsQueryable<T>();
        }

        public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, Func<IQueryable<T>, IIncludableQueryable<T, object>> include) where T : class
        {
            if (include != null)
            {
                query = include(query);
            }

            return query;
        }
    }
}

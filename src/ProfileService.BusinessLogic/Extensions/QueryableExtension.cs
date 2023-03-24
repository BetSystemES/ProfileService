using ProfileService.BusinessLogic.Entities;

namespace ProfileService.BusinessLogic.Extensions
{
    public static class QueryableExtension
    {
        public static IQueryable<T> SkipTake<T>(this IQueryable<T> query, int? skip, int? take)
        {
            if (skip.HasValue && take.HasValue)
            {
                return query
                    .Skip(skip.Value)
                    .Take(take.Value);
            }

            return query;
        }

        public static IQueryable<T> OrderByFunc<T>(this IQueryable<T> query, Func<IQueryable<T>, IOrderedQueryable<T>> order)
        {
            if (order is not null)
            {
                return query
                    .OrderBy(x => order);
            }

            return query;
        }
    }
}

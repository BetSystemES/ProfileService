using ProfileService.BusinessLogic.Entities;

namespace ProfileService.BusinessLogic.Extensions
{
    public static class QueryableExtension
    {
        public static IQueryable<Bonus> SkipTake(this IQueryable<Bonus> query, int? skip, int? take)
        {
            if (skip.HasValue && take.HasValue)
            {
                return query
                    .Skip(skip.Value)
                    .Take(take.Value);
            }

            return query;
        }

        public static IQueryable<Bonus> OrderByFunc(this IQueryable<Bonus> query, Func<IQueryable<Bonus>, IOrderedQueryable<Bonus>> order)
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

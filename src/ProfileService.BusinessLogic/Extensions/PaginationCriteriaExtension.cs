using ProfileService.BusinessLogic.Models.Criterias;

namespace ProfileService.BusinessLogic.Extensions
{
    public static class PaginationCriteriaExtension
    {
        public static (int? skip, int? take) GetPaginationCriteria(this PaginationCriteria criteria)
        {
            if (criteria.PageNumber.HasValue && criteria.PageSize.HasValue)
            {
                var skip = (criteria.PageNumber - 1) * criteria.PageSize;
                var take = criteria.PageSize;
                return (skip, take);
            }

            return (null, null);
        }
    }
}

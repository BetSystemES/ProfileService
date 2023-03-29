using System.Linq.Expressions;
using ProfileService.BusinessLogic.Entities;
using ProfileService.BusinessLogic.Models.Criterias;
using ProfileService.BusinessLogic.Helpers;

namespace ProfileService.BusinessLogic.Extensions
{
    public static class PredicateBuilderHelperExtension
    {
        public static Expression<Func<Bonus, bool>> FilterPredicateWithDateExtension(this Expression<Func<Bonus, bool>> predicate, FilterCriteria filter)
        {
            if (filter.StartDate.HasValue && filter.EndDate.HasValue)
            {
                if (filter.StartDate.Value == filter.EndDate.Value)
                {
                    return predicate.And(x => x.CreateDate == filter.StartDate.Value);
                }

                return predicate.And(x => x.CreateDate.Between(filter.StartDate.Value, filter.EndDate.Value));
            }

            if (filter.StartDate.HasValue && filter.EndDate == null)
            {
                return predicate.And(x => x.CreateDate > filter.StartDate.Value);
            }

            if (filter.EndDate.HasValue && filter.StartDate == null)
            {
                return predicate.And(x => x.CreateDate < filter.EndDate.Value);
            }

            return predicate;
        }
    }
}

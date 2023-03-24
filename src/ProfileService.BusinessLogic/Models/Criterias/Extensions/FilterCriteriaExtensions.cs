namespace ProfileService.BusinessLogic.Models.Criterias.Extensions
{
    public static class FilterCriteriaExtensions
    {
        public static void Apply(this FilterCriteria filterCriteria, bool isReadyToUse)
        {
            if (filterCriteria is null) filterCriteria = new();
            filterCriteria.IsEnabled = isReadyToUse;
        }
    }
}

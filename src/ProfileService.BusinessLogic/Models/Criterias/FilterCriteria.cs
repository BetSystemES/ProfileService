using ProfileService.BusinessLogic.Models.Enums;

namespace ProfileService.BusinessLogic.Models.Criterias
{
    public class FilterCriteria : OrderCriteria
    {
        public List<Guid>? UserIds { get; set; }
        public bool? IsEnabled { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public string? SearchCriteria { get; set; }
    }
}

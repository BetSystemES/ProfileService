namespace ProfileService.BusinessLogic.Models.Criterias
{
    public class FilterCriteria : OrderCriteria
    {
        public List<Guid>? UserIds { get; set; }
        public bool? IsEnabled { get; set; }
        public string? SearchCriteria { get; set; }
    }
}

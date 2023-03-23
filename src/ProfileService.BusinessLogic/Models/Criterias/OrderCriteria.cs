using ProfileService.BusinessLogic.Models.Enums;

namespace ProfileService.BusinessLogic.Models.Criterias
{
    public class OrderCriteria : PaginationCriteria
    {
        public string? ColumnName { get; set; }
        public OrderDirection? SortDirection { get; set; }
    }
}

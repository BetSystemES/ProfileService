using ProfileService.BusinessLogic.Models.Enums;

namespace ProfileService.BusinessLogic.Models.Criterias
{
    public class OrderCriteria : PaginationCriteria
    {
        public string? ColumnName { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}

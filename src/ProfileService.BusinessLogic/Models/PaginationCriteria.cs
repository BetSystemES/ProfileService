namespace ProfileService.BusinessLogic.Models
{
    public class PaginationCriteria
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }

    public class PaginationCriteriaResponse<T>  where T: class 
    {
        public int TotalCount { get; set; } //Filter-> Count-> Pagination -> Responce
        public List<T> Data { get; set; }
    }

    public class OrderCriteria : PaginationCriteria
    {
        public string? ColumnName { get; set; }
        public OrderDirection? SortDirection { get; set; }
    }

    public enum OrderDirection
    {
        Unspecified = 0,
        Ascending = 1,
        Descending = 2
    }

    public class FilterCriteria : OrderCriteria
    {
        public List<Guid>? UserIds { get; set; }
        public bool? IsEnabled { get; set; }
        public string? SearchCriteria { get; set; }
    }
}
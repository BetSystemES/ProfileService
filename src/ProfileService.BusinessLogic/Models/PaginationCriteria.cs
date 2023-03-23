using System.ComponentModel.DataAnnotations;

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

    public class SortCriteria : PaginationCriteria
    {
        public string? ColumnName { get; set; }
        public SortDirection? SortDirection { get; set; }
    }

    public enum SortDirection
    {
        Asc,
        Desc
    }

    public class FilterCriteria : SortCriteria
    {
        public List<Guid>? UserIds { get; set; }
        public bool? IsEnabled { get; set; }
        public string? SearchCriteria { get; set; }
    }

}
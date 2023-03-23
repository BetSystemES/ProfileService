namespace ProfileService.BusinessLogic.Models
{
    public class PagedResponse<T>  where T: class 
    {
        public int TotalCount { get; set; } //Filter-> Count-> Pagination -> Responce
        public List<T> Data { get; set; }
    }
}
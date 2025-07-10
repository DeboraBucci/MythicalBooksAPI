namespace MythicalBooksAPI.Dtos
{
    public class PagedResultDto<T>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 2;
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<T> Data { get; set; } = Enumerable.Empty<T>();
    }
}

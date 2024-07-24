namespace BusinessLogic.Common.Models
{
    public class PaginationMetadata(int count, int pageNumber, int pageSize)
    {
        public int TotalCount { get; set; } = count;
        public int CurrentPage { get; set; } = pageNumber;
        public int TotalPages { get; set; } = (int)Math.Ceiling(count / (double) pageSize);
        public int PageSize { get; set; } = pageSize;
    }
}

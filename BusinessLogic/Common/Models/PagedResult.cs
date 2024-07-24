
namespace BusinessLogic.Common.Models
{
    public class PagedResult<T>(IList<T> items, int count, int pageNumber, int pageSize)
    {
        public IList<T> Items { get; set; } = items;
        public PaginationMetadata PaginationMetadata => new PaginationMetadata(count, pageNumber, pageSize);
    }
}

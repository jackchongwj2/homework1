using Microsoft.EntityFrameworkCore.Metadata.Internal;
using homework1.ViewModels;


namespace homework1.ViewModels
{
    public class PaginationViewModel<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public string SearchTerm { get; set; }
    }
}

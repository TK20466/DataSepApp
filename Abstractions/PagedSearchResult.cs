namespace Abstractions
{
    public class PagedSearchResult<T> : SearchResults<T> where T : class
    {
        public int Count { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }
    }
}

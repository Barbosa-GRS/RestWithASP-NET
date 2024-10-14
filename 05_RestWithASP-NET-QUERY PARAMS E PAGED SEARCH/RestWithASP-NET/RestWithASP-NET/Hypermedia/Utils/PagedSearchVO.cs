using RestWithASP_NET.Hypermedia.Abstract;

namespace RestWithASP_NET.Hypermedia.Utils
{
    public class PagedSearchVO<T> where T : ISupportsHyperMedia
    {
        public int CurrentPage {  get; set; }
        public int PagedSize {  get; set; }
        public int TotalResults {  get; set; }
        public string  SortFilds {  get; set; }
        public string  SortDirections {  get; set; }
        public Dictionary<string, Object> Filters { get; set; }
        public List<T> List { get; set; }

        public PagedSearchVO(){ }

        public PagedSearchVO(int currentPage, int pagedSize, string sortFilds, string sortDirections)
        {
            CurrentPage = currentPage;
            PagedSize = pagedSize;
            SortFilds = sortFilds;
            SortDirections = sortDirections;
        }

        public PagedSearchVO(int currentPage, int pagedSize, string sortFilds, string sortDirections, Dictionary<string, object> filters)
        {
            Filters = filters;
            CurrentPage = currentPage;
            PagedSize = pagedSize;
            SortFilds = sortFilds;
            SortDirections = sortDirections;
        }

        public PagedSearchVO(int currentPage, string sortFilds, string sortDirections) 
            : this (currentPage, 10, sortFilds, sortDirections)   {   }

        public int GetCurrentPage()
        {
            return CurrentPage == 0 ? 2 : CurrentPage;
        }

        public int GetPageSize()
        {
            return PagedSize == 0 ? 10: PagedSize;
        }

    }
}

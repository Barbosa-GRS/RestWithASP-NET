using RestWithASP_NET.Data.VO;
using RestWithASP_NET.Hypermedia.Utils;
using RestWithASP_NET.Model;

namespace RestWithASP_NET.Business
{
    public interface IBookBusiness
    {
        BookVO Create(BookVO book);
        BookVO FindByID(long id);
        List<BookVO> FindAll();
        PagedSearchVO<BookVO> FindWithPagedSearch(
           string title, string sortDirection, int pageSize, int page);
        BookVO Update(BookVO book);
        void Delete(long id);
    }
}

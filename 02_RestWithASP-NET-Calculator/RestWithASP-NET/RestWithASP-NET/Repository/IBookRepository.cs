using RestWithASP_NET.Model;
using RestWithASP_NET.Model.Context;

namespace RestWithASP_NET.Repository
{
    public interface IBookRepository
    {
        Book Create(Book book);
        Book FindByID(long id);
        List<Book> FindAll();
        Book Update(Book book);
        void Delete(long id);
        bool Exists(long id);

    }
}

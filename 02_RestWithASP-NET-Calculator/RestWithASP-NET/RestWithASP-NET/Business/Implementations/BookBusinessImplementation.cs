using RestWithASP_NET.Data.Converter.Implementations;
using RestWithASP_NET.Data.VO;
using RestWithASP_NET.Model;
using RestWithASP_NET.Repository;


namespace RestWithASP_NET.Business.Implementations
{
    public class BookBusinessImplementation : IBookBusiness
    {
        private readonly IRepository<Book> _repository;
        private readonly BookConverter _converter;

        public BookBusinessImplementation(IRepository<Book> repository)
        {
            _repository = repository;
            _converter = new BookConverter();
        }

        public List<BookVO> FindAll()
        {
            /* sem banco de dados
             List<Person> persons = new List<Person>();
             for (int i = 0; i < 8; i++)
             {
                 Person person = MockPerson(i);
                 persons.Add(person);
             }*/
            return _converter.Parse(_repository.FindAll());
        }

        public BookVO FindByID(long id)
        {
            return _converter.Parse(_repository.FindByID(id));

        }

        public BookVO Create(BookVO book)
        {
            var bookEntity = _converter.Parse(book);
            bookEntity = _repository.Create(bookEntity);
            return _converter.Parse(bookEntity);
        }

        public BookVO Update(BookVO book)
        {
            var bookEntity = _converter.Parse(book);
            bookEntity = _repository.Update(bookEntity);
            return _converter.Parse(bookEntity);
        }



        public void Delete(long id)
        {
            _repository.Delete(id);
        }


    }

}

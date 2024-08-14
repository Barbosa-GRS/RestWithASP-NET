using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using RestWithASP_NET.Business;
using RestWithASP_NET.Model;
using RestWithASP_NET.Model.Context;
using RestWithASP_NET.Repository;
using System;

namespace RestWithASP_NET.Business.Implementations
{
    public class BookBusinessImplementation : IBookBusiness
    {
        private readonly IBookRepository _repository;

        public BookBusinessImplementation(IBookRepository repository)
        {
            _repository = repository;
        }

        public List<Model.Book> FindAll()
        {
            /* sem banco de dados
             List<Person> persons = new List<Person>();
             for (int i = 0; i < 8; i++)
             {
                 Person person = MockPerson(i);
                 persons.Add(person);
             }*/
            return _repository.FindAll();
        }

        public Model.Book FindByID(long id)
        {
            return _repository.FindByID(id);

        }

        public Model.Book Create(Model.Book book)
        {
            return _repository.Create(book);
        }

        public Model.Book Update(Model.Book book)
        {
            return _repository.Update(book);
        }



        public void Delete(long id)
        {
            _repository.Delete(id);
        }


    }

}

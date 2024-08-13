using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using RestWithASP_NET.Business;
using RestWithASP_NET.Model;
using RestWithASP_NET.Model.Context;
using RestWithASP_NET.Repository;
using System;

namespace RestWithASP_NET.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IPersonRepository  _repository;

        public PersonBusinessImplementation(IPersonRepository repository) 
        {
            _repository = repository;
        }

        public List<Person> FindAll()
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

        public Person FindByID(long id)
        {
            return _repository.FindByID(id);

        }

        public Person Create(Person person)
        {
            return _repository.Create(person);
        }

        public Person Update(Person person)
        {
            return _repository.Update(person);
        }

        

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
                   
               
    }

    }

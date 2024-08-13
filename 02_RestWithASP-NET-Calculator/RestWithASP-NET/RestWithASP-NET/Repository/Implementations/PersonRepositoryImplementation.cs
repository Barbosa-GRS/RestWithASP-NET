using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using RestWithASP_NET.Model;
using RestWithASP_NET.Model.Context;
using System;

namespace RestWithASP_NET.Repository.Implementations
{
    public class PersonRepositoryImplementation : IPersonRepository
    {
        private MySQLContext _context;

        public PersonRepositoryImplementation(MySQLContext context) 
        {
            _context = context;
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
            return _context.Persons.ToList();
        }

        public Person FindByID(long id)
        {
            return _context.Persons.SingleOrDefault(p => p.Id.Equals(id));

        }


        public Person Create(Person person)
        {
            try
            {
                _context.Add(person);
                _context.SaveChanges();
            }
            catch (Exception )
            {

                throw ;
            }
            return person;
        }

        public Person Update(Person person)
        {
            if (!Exists(person.Id)) return null;
            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(person.Id));
            if (result != null)
            {

                try
                {
                    _context.Entry(result).CurrentValues.SetValues(person);
                    _context.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return person;
        }

        public bool Exists(long id)
        {
            return _context.Persons.Any(p => p.Id.Equals(id));
        }

        public void Delete(long id)
        {
            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
            if (result != null)
            {

                try
                {
                    _context.Persons.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
        
              
               
    }

    }

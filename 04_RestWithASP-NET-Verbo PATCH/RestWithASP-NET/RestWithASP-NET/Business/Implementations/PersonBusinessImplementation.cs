using RestWithASP_NET.Data.Converter.Implementations;
using RestWithASP_NET.Model;
using RestWithASP_NET.Repository;
using RestWithASP_NET.Data.VO;

namespace RestWithASP_NET.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {

        private readonly IPersonRepository _repository;

        private readonly PersonConverter _converter;

        public PersonBusinessImplementation(IPersonRepository repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public List<PersonVO> FindAll()
        {
            /* sem banco de dados
             List<Person> persons = new List<PersonVO>();
             for (int i = 0; i < 8; i++)
             {
                 PersonVO person = MockPerson(i);
                 persons.Add(person);
             }*/
            return _converter.Parse(_repository.FindAll());
        }

        public PersonVO FindByID(long id)
        {
            return _converter.Parse(_repository.FindByID(id));

        }

        public PersonVO Create(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Create(personEntity);
            return _converter.Parse(personEntity);
        }

        public PersonVO Update(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Update(personEntity);
            return _converter.Parse(personEntity);
        }

        public PersonVO Desable(long id)
        {
            var personEntity = _repository.Desable(id);
            return _converter.Parse(personEntity);

        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
              
    }

    }

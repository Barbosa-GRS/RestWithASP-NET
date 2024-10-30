using RestWithASP_NET.Data.Converter.Implementations;
using RestWithASP_NET.Repository;
using RestWithASP_NET.Data.VO;
using RestWithASP_NET.Hypermedia.Utils;

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

        public PagedSearchVO<PersonVO> FindWithPagedSearch(
            string name, string sortDirection, int pageSize, int page)
        {
            var sort = (!string.IsNullOrEmpty(sortDirection)) && !sortDirection.Equals("desc") ? "asc" : "desc";
            var size = (pageSize < 1) ? 10 : pageSize;
            var offset = page > 0 ? (page - 1) * size : 0;

            string query = @"select * from person p where 1 = 1 ";
            if (!string.IsNullOrWhiteSpace(name)) query = query + $" and p.first_name like '%{name}%' ";
            query += $" order by p.first_name {sort} limit {size} offset {offset}";


            string countQuery = @"select count(*) from person p where 1 = 1 ";
            if (!string.IsNullOrWhiteSpace(name)) countQuery = countQuery + $" and p.first_name like '%{name}%' ";

            var persons = _repository.FindWithPagedSearch(query);
            int totoalResults = _repository.GetCount(countQuery);
            return new PagedSearchVO<PersonVO>
            {
                CurrentPage = page,
                List =  _converter.Parse(persons),
                PageSize = size,
                SortDirections = sort,
                TotalResults = totoalResults,
            };
        }

        public PersonVO FindByID(long id)
        {
            return _converter.Parse(_repository.FindByID(id));

        }

        public List<PersonVO> FindByName(string firstName, string lastName)
        {
            var list = _repository.FindByName(firstName, lastName);
            return _converter.Parse(list);
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

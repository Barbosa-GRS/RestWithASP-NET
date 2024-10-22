using RestWithASP_NET.Data.VO;
using RestWithASP_NET.Hypermedia.Utils;

namespace RestWithASP_NET.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO person);
        PersonVO FindByID(long id);
        List<PersonVO> FindByName(string firstName, string lastName);
        List<PersonVO> FindAll();
        PagedSearchVO<PersonVO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page);

        PersonVO Update(PersonVO person);
        PersonVO Desable(long id);
        void Delete(long id);


    }
}

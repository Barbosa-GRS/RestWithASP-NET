using RestWithASP_NET.Model;

namespace RestWithASP_NET.Repository
{

    public interface IPersonRepository : IRepository<Person>
    {
       Person Desable(long id);
    }
}

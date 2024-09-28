using RestWithASP_NET.Model;
using RestWithASP_NET.Model.Context;
using RestWithASP_NET.Repository.Generic;

namespace RestWithASP_NET.Repository
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository 
    {
        public PersonRepository(MySQLContext context) : base(context) { }

        public Person Desable(long id)
        {
            if (!_context.Persons.Any(p => p.Id.Equals(id))) return null;
            var user = _context.Persons.SingleOrDefault(p => p.Id == id);
            if (user != null) 
            {
                user.Enabled = false;

                try
                {
                    _context.Entry(user).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return user;
        }
    }
}

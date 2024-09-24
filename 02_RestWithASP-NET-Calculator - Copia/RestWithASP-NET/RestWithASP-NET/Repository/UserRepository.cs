using RestWithASP_NET.Data.VO;
using RestWithASP_NET.Model;
using RestWithASP_NET.Model.Context;
using System.Data;
using System.Security.Cryptography;
using System.Text;


namespace RestWithASP_NET.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MySQLContext _context;

        public UserRepository(MySQLContext context)
        {
            _context = context;
        }

        public User ValidateCredentials(UserVO user)
        {
            var pass = ComputeHash(user.Password, SHA256.Create());
            return _context.Users.FirstOrDefault(u => (u.UserName == user.UserName) && (u.Password == pass));
        }

        public User RefreshUserInfo( User user)
        {
            if (!_context.Users.Any(u => u.Id.Equals(user.Id)))return null; // se não achar ninguem com o msm ID do user recebido retorna null

            var result = _context.Users.SingleOrDefault(p => p.Id.Equals(user.Id)); // se encontrar alguem com o msm ID guarda em result
            if (result != null) // se result dif de null tenta atualixar as infos do usuario fazendo um update
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return result;
        }

        private string ComputeHash(string input, HashAlgorithm algorithm)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            var builder = new StringBuilder();

            foreach (var item in hashedBytes)
            {
                builder.Append(item.ToString("x2"));
            }
            return builder.ToString();
        }

    }
}

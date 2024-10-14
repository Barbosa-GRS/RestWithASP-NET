using RestWithASP_NET.Data.VO;
using RestWithASP_NET.Model;

namespace RestWithASP_NET.Repository
{

    public interface IUserRepository
    {
        User ValidateCredentials(UserVO user);

        User ValidateCredentials(string username);

        bool RevokeToken(string username);

        User RefreshUserInfo(User user);
    }
}

using RestWithASP_NET.Data.VO;
using RestWithASP_NET.Model;

namespace RestWithASP_NET.Repository
{

    public interface IUserRepository
    {
        
        User ValidateCredentials(UserVO user);

        User RefreshUserInfo(User user);

    }
}

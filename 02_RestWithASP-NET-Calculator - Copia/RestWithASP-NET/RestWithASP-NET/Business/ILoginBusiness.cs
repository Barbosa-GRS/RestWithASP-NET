using RestWithASP_NET.Data.VO;

namespace RestWithASP_NET.Business
{
    public interface ILoginBusiness
    {
        TokenVO ValidationCredentials(UserVO user);
    }
}

using AuthorizationApi.Models.Requests;

namespace AuthorizationApi.Services
{
    public interface IAuthService
    {
        string Register(UserRegisterRequest request);
        string LogIn(UserRegisterRequest request);
    }

    public class AuthService : IAuthService
    {
        public string Register(UserRegisterRequest request)
        {
            throw new NotImplementedException();
        }

        public string LogIn(UserRegisterRequest request)
        {
            throw new NotImplementedException();
        }
    }
}

using AuthorizationApi.Domain;

namespace AuthorizationApi.Models.Requests
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRolesEnum Role { get; set; }
    }
}

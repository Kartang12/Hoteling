using Microsoft.AspNetCore.Identity;

namespace AuthorizationApi.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PWHash { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public UserRolesEnum Role { get; set; }
    }
}

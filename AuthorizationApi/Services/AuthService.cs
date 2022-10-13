using AuthorizationApi.Contracts.Requests;
using AuthorizationApi.Contracts.Responses;
using AuthorizationApi.DbContext;
using AuthorizationApi.Domain;
using AuthorizationApi.Models.Requests;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;

namespace AuthorizationApi.Services
{
    public interface IAuthService
    {
        Task<RegistrationResponse> RegisterAsync(RegisterRequest request);
        Task<AuthenticationResponse> LogInAsync(LoginRequest request);
        Task<AuthenticationResponse> RefreshAsync(RefreshTokenRequest request);
    }

    public class AuthService : IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly AuthDbContext _userContext;

        public AuthService(AuthDbContext userContext, ITokenService tokenService)
        {
            _userContext = userContext;
            _tokenService = tokenService;
        }

        public async Task<RegistrationResponse> RegisterAsync(RegisterRequest request)
        {
            var userExists = await _userContext.Set<User>("users").FirstOrDefaultAsync(x => (x.Email == request.Email) && (x.PWHash == pwHash));
            if (userExists != null)
                return new RegistrationResponse
                {
                    Success = false,
                    Errors = new[] { "User with this email already exist" }
                };
            var pwHash = SHA256.Create(request.Password).ToString();
            var refreshToken = _tokenService.GenerateRefreshToken();

            var newUser = new User()
            {
                Email = request.Email,
                PWHash = pwHash,
                RefreshToken = refreshToken,
                RefreshTokenExpiryTime = DateTime.Now.AddDays(7)
            };

            return new RegistrationResponse
            {
                Success = true
            };
        }

        public async Task<AuthenticationResponse> LogInAsync(LoginRequest request)
        {
            var pwHash = SHA256.Create(request.Password).ToString();
            var user = await _userContext.Set<User>("users").FirstOrDefaultAsync(x => (x.Email == request.Email) && (x.PWHash == pwHash));
            if (user is null)
                return new AuthenticationResponse
                {
                    Success = false,
                    Errors = new[] { "Invalid user data" }
                }; 
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, request.Email),
                new Claim(ClaimTypes.Role, "User")
            };
            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            _userContext.SaveChangesAsync();
            return new AuthenticationResponse
            {
                Token = accessToken,
                RefreshToken = refreshToken,
                Success = true
            };
        }

        public async Task<AuthenticationResponse> RefreshAsync(RefreshTokenRequest request)
        {
            string accessToken = request.AccessToken;
            string refreshToken = request.RefreshToken;
            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            var email = principal.Identity.Name;
            var user = await _userContext.Set<User>("users").SingleOrDefaultAsync(x => x.Email == email);
            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                return new AuthenticationResponse()
                {
                    Success = false,
                    Errors = new[] { "Invalid client request" }
                };
            var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;
            _userContext.SaveChangesAsync();
            return new AuthenticationResponse()
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken,
                Success = true
            };
        }
    }
}

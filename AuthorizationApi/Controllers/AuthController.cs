using AuthorizationApi.Contracts.Requests;
using AuthorizationApi.Models.Requests;
using AuthorizationApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationApi.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService tokenService)
        {
            _authService = tokenService;
        }
        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (request is null)
                return BadRequest("Invalid client request");
            var result = await _authService.LogInAsync(request);
            return result.Success ? 
                Ok(result) : 
                Unauthorized(result);
        }
        
        [HttpPost, Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (request is null)
                return BadRequest("Invalid client request");
            var result = await _authService.RegisterAsync(request);
            return result.Success ? 
                Ok(result) : 
                Unauthorized(result);
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> Refresh(RefreshTokenRequest request)
        {
            if (request is null)
                return BadRequest("Invalid client request");
            var result = await _authService.RefreshAsync(request);
            return result.Success ?
                Ok(result) :
                Unauthorized(result);
        }
    }
}

using AuthorizationApi.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace AuthorizationApi.Controllers
{
    public class AuthController : Controller
    {
        [HttpPost]
        public IActionResult Register(UserRegisterRequest request)
        {

        }
    }
}

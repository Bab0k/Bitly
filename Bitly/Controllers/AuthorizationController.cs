namespace Bitly.Controllers
{
    using Bitly.Services.AuthService;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Route("api/[Controller]")]
    public class AuthorizationController : Controller
    {
        private readonly IAuthService _authorizationService;

        public AuthorizationController(IAuthService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<IActionResult> Login(string login, string password)
        {
            var result = await _authorizationService.Login(login, password);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Registration(string login, string password)
        {
            await _authorizationService.Registration(login, password);

            return Ok();

        }
    }
}
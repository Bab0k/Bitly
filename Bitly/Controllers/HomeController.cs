namespace Bitly.Controllers
{
    using Bitly.Services.UserService;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("/{url}")]
        public async Task<IActionResult> GetUserByUrl(string url)
        {
            var result = await _userService.GetUserByUrl(url);

            return Ok(result);
        }
    }
}
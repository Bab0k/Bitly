namespace Bitly.Controllers
{
    using Bitly.Services.UrlService;
    using Bitly.Services.UserService;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Route("api/[Controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _userService.GetAllUsers();

            return Ok(result);
        }

        [HttpGet("get-url-by-id/{id}")]
        public async Task<IActionResult> GetShortUrlByUserId(int id)
        {
            var user = await _userService.GetUserById(id);

            var result = UrlHelper.GetShortUrl(user);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(string name)
        {
            await _userService.AddUser(name);

            return Ok();
        }
    }
}
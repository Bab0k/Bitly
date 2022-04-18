namespace Bitly.Controllers
{
    using Bitly.Services.LinkedLinksService;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class HomeController : Controller
    {
        private readonly ILinkedLinksService _linkedLinksService;

        public HomeController(ILinkedLinksService linkedLinksService)
        {
            _linkedLinksService = linkedLinksService;
        }

        [HttpGet("/{url}")]
        public async Task<RedirectResult> ShortLinkTransition(string url)
        {
            var result = await _linkedLinksService.ShortLinkTransition(url);

            return Redirect(result);
        }
    }
}
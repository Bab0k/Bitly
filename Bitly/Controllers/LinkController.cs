namespace Bitly.Controllers
{
    using Bitly.Models;
    using Bitly.Services.LinkedLinksService;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Route("api/[Controller]")]
    public class LinkController : Controller
    {
        private readonly ILinkedLinksService _linkedLinksService;

        public LinkController(ILinkedLinksService linkedLinksService)
        {
            _linkedLinksService = linkedLinksService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var result = await _linkedLinksService.GetAll();

            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddLink(string link)
        {
            var result = await _linkedLinksService.AddLink(link);

            return Ok(result);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateLink(int id, string link)
        {
            await _linkedLinksService.UpdateLink(id, link);

            return Ok();
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteLink(int id)
        {
            await _linkedLinksService.DeleteLinkById(id);

            return Ok();
        }
    }
}
namespace Bitly.Services.LinkedLinksService
{
    using Bitly.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILinkedLinksService
    {
        Task<IEnumerable<LinkedLink>> GetAll();

        Task<string> AddLink(string link);

        Task<string> ShortLinkTransition(string link);
    }
}
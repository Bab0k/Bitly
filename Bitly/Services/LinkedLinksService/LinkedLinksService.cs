namespace Bitly.Services.LinkedLinksService
{
    using Bitly.Database;
    using Bitly.Models;
    using Bitly.Services.UserService;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class LinkedLinksService : ILinkedLinksService
    {
        private const int ColumnBase = 26;
        private const int DigitMax = 7;
        private const string Digits = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private readonly BaseDbContext _baseDbContext;
        private readonly IUserService _userService;

        public LinkedLinksService(BaseDbContext baseDbContext, IUserService userService)
        {
            _baseDbContext = baseDbContext;
            _userService = userService;
        }

        public async Task<string> ShortLinkTransition(string link)
        {
            var result = await _baseDbContext.LinkedLinks.FirstOrDefaultAsync(u => u.ShortLink == Consts.BASE_URL + link);

            return result?.Link ?? Consts.BASE_URL + "Home/Error";
        }

        public async Task<string> AddLink(string link)
        {
            var id = await _baseDbContext.LinkedLinks.Select(u => u.Id).MaxAsync();

            var newlink = new LinkedLink
            {
                Id = id + 1,
                Link = link,
                UserId = _userService.UserId,
                ShortLink = Consts.BASE_URL + IndexToColumn(id + 1),
            };

            await _baseDbContext.LinkedLinks.AddAsync(newlink);

            await _baseDbContext.SaveChangesAsync();

            return newlink.ShortLink;
        }

        public async Task<IEnumerable<LinkedLink>> GetAll()
        {
            var result = await _baseDbContext.LinkedLinks.Where(u => u.UserId == _userService.UserId).ToListAsync();

            return result;
        }

        private static string IndexToColumn(int index)
        {
            if (index <= 0)
                throw new IndexOutOfRangeException("index must be a positive number");

            if (index <= ColumnBase)
                return Digits[index - 1].ToString();

            var sb = new StringBuilder().Append(' ', DigitMax);
            var current = index;
            var offset = DigitMax;
            while (current > 0)
            {
                sb[--offset] = Digits[--current % ColumnBase];
                current /= ColumnBase;
            }
            return sb.ToString(offset, DigitMax - offset);
        }
    }
}
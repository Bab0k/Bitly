namespace Bitly.Services.UrlService
{
    using Bitly.Models;
    using System.Linq;

    public class UrlHelper
    {
        public static int FindUserIdByShortUrl(string url)
        {
            return int.Parse(string.Join(string.Empty, url.Select(u => int.Parse(u.ToString()) - 1)).TrimStart('0'));
        }

        public static string GetShortUrl(User user)
        {
            return $"{Consts.BASE_URL}{ShortUrl(user.Id.ToString())}";
        }

        private static string ShortUrl(string id)
        {
            return string.Join(string.Empty, id
                .PadLeft(8, '0')
                .Select(u => int.Parse(u.ToString()) + 1));
        }
    }
}
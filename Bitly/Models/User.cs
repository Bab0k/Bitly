namespace Bitly.Models
{
    using System.Linq;

    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public IQueryable<LinkedLink> LinkedLinks { get; set; }
    }
}
namespace Bitly.Models
{
    public class LinkedLink
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Link { get; set; }
        public string ShortLink { get; set; }
    }
}
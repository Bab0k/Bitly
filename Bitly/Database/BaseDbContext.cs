namespace Bitly.Database
{
    using Bitly.Models;
    using Microsoft.EntityFrameworkCore;

    public class BaseDbContext : DbContext
    {
        public BaseDbContext(DbContextOptions<BaseDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<LinkedLink> LinkedLinks { get; set; }
    }
}
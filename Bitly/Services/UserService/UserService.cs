namespace Bitly.Services.UserService
{
    using Bitly.Database;
    using Bitly.Models;
    using Bitly.Services.UrlService;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class UserService : IUserService
    {
        private readonly BaseDbContext _baseDbContext;

        public UserService(BaseDbContext baseDbContext)
        {
            _baseDbContext = baseDbContext;
        }

        public async Task AddUser(string name)
        {
            using (var transaction = _baseDbContext.Database.BeginTransaction())
            {
                try
                {
                    var result = await _baseDbContext.Users
                        .AddAsync(new User
                        {
                            Name = name,
                        });

                    await _baseDbContext.SaveChangesAsync();
                    transaction.Commit();
                }
                catch
                {
                    throw new Exception("Something went wrong");
                }
            }
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            IEnumerable<User> result = new List<User>();

            try
            {
                result = await _baseDbContext.Users.ToListAsync();
            }
            catch
            {
                throw new Exception("Something went wrong");
            }

            return result;
        }

        public async Task<User> GetUserById(int id)
        {
            User result = new User();

            try
            {
                result = await _baseDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            }
            catch
            {
                throw new Exception("Something went wrong");
            }

            return result;
        }

        public async Task<User> GetUserByUrl(string url)
        {
            User result = new User();

            try
            {
                var id = UrlHelper.FindUserIdByShortUrl(url);

                result = await _baseDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            }
            catch (Exception)
            {
                throw new Exception("Something went wrong");
            }

            return result;
        }
    }
}
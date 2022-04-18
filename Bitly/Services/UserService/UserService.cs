namespace Bitly.Services.UserService
{
    using Bitly.Database;
    using Bitly.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using System.Security.Claims;

    public class UserService : IUserService
    {
        private readonly BaseDbContext _baseDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(BaseDbContext baseDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _baseDbContext = baseDbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public int UserId => int.Parse(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value);

        public async Task AddUser(string login, string password)
        {
            try
            {
                var result = await _baseDbContext.Users
                    .AddAsync(new User
                    {
                        Login = login,
                        Password = password,
                    });

                await _baseDbContext.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Something went wrong");
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
    }
}
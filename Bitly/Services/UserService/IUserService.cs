namespace Bitly.Services.UserService
{
    using Bitly.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();

        Task<User> GetUserById(int id);

        Task<User> GetUserByUrl(string url);

        Task AddUser(string name);
    }
}
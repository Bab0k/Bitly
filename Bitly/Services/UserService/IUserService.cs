namespace Bitly.Services.UserService
{
    using Bitly.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();

        Task AddUser(string login, string password);

        int UserId { get; }
    }
}
namespace Bitly.Services.AuthService
{
    using System.Threading.Tasks;

    public interface IAuthService
    {
        Task<string> Login(string user, string password);

        Task Registration(string user, string password);
    }
}
namespace Bitly.Services.AuthService
{
    using Bitly.Database;
    using Bitly.Models;
    using Bitly.Services.UserService;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    public class AuthService : IAuthService
    {
        private readonly TokensOptions _tokenOptions;
        private readonly BaseDbContext _baseDbContext;
        private readonly IUserService _userService;

        public AuthService(IOptions<TokensOptions> tokenOptions, BaseDbContext baseDbContext, IUserService userService)
        {
            _tokenOptions = tokenOptions.Value;
            _baseDbContext = baseDbContext;
            _userService = userService;
        }

        public async Task<string> Login(string login, string password)
        {
            var user = await _baseDbContext.Users.FirstOrDefaultAsync(u => u.Login == login && password == u.Password);

            if (user == null)
            {
                throw new Exception("UserNotFound");
            }

            string accessToken = GetAccessToken(user);

            return accessToken;
        }

        public async Task Registration(string login, string password)
        {
            var user = await _baseDbContext.Users.FirstOrDefaultAsync(u => u.Login == login);

            if (user != null)
            {
                throw new Exception("User Exist");
            }

            await _userService.AddUser(login, password);
        }

        private string GetAccessToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _tokenOptions.Issuer,
                _tokenOptions.Audience,
                claims: new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    },
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
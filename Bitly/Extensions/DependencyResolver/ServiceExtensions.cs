namespace Bitly.Extensions.DependencyResolver
{
    using Bitly.Models;
    using Bitly.Services.AuthService;
    using Bitly.Services.LinkedLinksService;
    using Bitly.Services.UserService;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Text;

    public static class ServiceExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ILinkedLinksService, LinkedLinksService>();
        }

        public static void AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<TokensOptions>()
                    .Bind(configuration.GetSection("Tokens"));
        }

        public static void AddCustomJwt(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["Tokens:Issuer"], // site that makes the token
                    ValidateIssuer = false, // TODO: change this to avoid forwarding attacks
                    ValidAudience = configuration["Tokens:Audience"], // site that consumes the token
                    ValidateAudience = false, // TODO: change this to avoid forwarding attacks
                    ValidateIssuerSigningKey = true, // verify signature to avoid tampering
                    ValidateLifetime = true, // validate the expiration
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetSection("Tokens:Key").Value)),
                    ClockSkew = TimeSpan.Zero // tolerance for the expiration date
                };
            });
        }
    }
}
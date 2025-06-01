using System.Text;
using Application.Authentication;
using Application.Users;
using Domain.Users;
using Infrastructure.Options;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Persistence.Contexts;

namespace Infrastructure.IoC;

public static class InfrastructureInjector
{
    public static IServiceCollection InjectInfrastructureDependencies(
        this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        AuthenticationOptions authenticationOptions = configuration
            .GetSection(nameof(AuthenticationOptions)).Get<AuthenticationOptions>()
            ?? throw new Exception("Authentication options not configured");

        serviceCollection.AddDefaultIdentity<User>(options =>
            options.SignIn.RequireConfirmedAccount = false)
            .AddEntityFrameworkStores<ApplicationContext>();

        serviceCollection.AddScoped<IUserService, UserService>();
        serviceCollection.AddScoped<IAuthService, AuthService>();

        SymmetricSecurityKey jwtKey = new(Encoding.UTF8.GetBytes(authenticationOptions.Secret));
        serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
                options.TokenValidationParameters = new()
                {
                    ValidateAudience = authenticationOptions.ValidateAudience,
                    ValidateIssuer = authenticationOptions.ValidateIssuer,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = jwtKey,
                    ClockSkew = TimeSpan.Zero
                });

        return serviceCollection;
    }
}
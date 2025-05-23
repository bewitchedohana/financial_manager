using Application.Users;
using Domain.Users;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;

namespace Infrastructure.IoC;

public static class InfrastructureInjector
{
    public static IServiceCollection InjectInfrastructureDependencies(
        this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDefaultIdentity<User>(options =>
            options.SignIn.RequireConfirmedAccount = false)
            .AddEntityFrameworkStores<ApplicationContext>();

        serviceCollection.AddScoped<IUserService, UserService>();

        return serviceCollection;
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;

namespace Persistence.IoC;

public static class PersistenceInjector
{
    public static IServiceCollection InjectPersistenceDependencies(
        this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("DefaultConnection");
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);

        serviceCollection.AddDbContext<ApplicationContext>(options =>
            options.UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention());

        return serviceCollection;
    }
}
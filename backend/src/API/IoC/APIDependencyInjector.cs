namespace API.IoC;

public static class APIDependencyInjector
{
    public static IServiceCollection InjectAPIDependencies(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddCors(options =>
        {
            options.AddPolicy(name: "AllowOrigin", policy =>
            {
                policy
                    .WithOrigins("http://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyOrigin();
            });
        });

        return serviceCollection;
    }
}

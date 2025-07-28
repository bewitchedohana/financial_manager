using Persistence.IoC;
using Infrastructure.IoC;
using API.IoC;

namespace API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.InjectPersistenceDependencies(builder.Configuration)
            .InjectInfrastructureDependencies(builder.Configuration)
            .InjectAPIDependencies();

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseCors("AllowOrigin");
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}

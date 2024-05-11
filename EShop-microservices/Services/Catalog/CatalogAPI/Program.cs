using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace CatalogAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var assembly = typeof(Program).Assembly;

        builder.Services.AddCarter();

        builder.Services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });

        builder.Services.AddValidatorsFromAssembly(assembly);

        builder.Services.AddMarten(options =>
        {
            options.Connection(builder.Configuration.GetConnectionString("Database")!);
        }).UseLightweightSessions();

        if (builder.Environment.IsDevelopment())
        {
            builder.Services.InitializeMartenWith<CatalogInitialData>();
        }

        builder.Services.AddExceptionHandler<CustomExceptionHandler>();
        builder.Services
            .AddHealthChecks()
            .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

        var app = builder.Build();

        app.MapGet("/", () => "Catalog API");
        
        app.MapCarter();

        app.UseExceptionHandler(_ =>
        {
        });

        app.UseHealthChecks("/health", new HealthCheckOptions()
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
        
        app.Run();
    }
}
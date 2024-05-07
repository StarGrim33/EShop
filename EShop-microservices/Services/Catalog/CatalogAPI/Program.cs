using Building_Blocks.Behaviors;

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
        });

        builder.Services.AddValidatorsFromAssembly(assembly);

        builder.Services.AddMarten(options =>
        {
            options.Connection(builder.Configuration.GetConnectionString("Database")!);
        }).UseLightweightSessions();

        var app = builder.Build();

        app.MapGet("/", () => "Catalog API");
        app.MapCarter();

        app.Run();
    }
}
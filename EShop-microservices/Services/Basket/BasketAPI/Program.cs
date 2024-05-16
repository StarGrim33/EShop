namespace BasketAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddExceptionHandler<CustomExceptionHandler>();
        builder.Services.RegisterServices(builder);

        var app = builder.Build();

        app.MapGet("/", () => "BasketAPI!");
        app.MapCarter();
        app.UseExceptionHandler(options => { });
        app.Run();
    }
}
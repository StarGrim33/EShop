namespace BasketAPI.DependencyInjection;

public static class DiServices
{
    public static void RegisterServices(this IServiceCollection services, WebApplicationBuilder builder)
    {
        var assembly = typeof(Program).Assembly;

        services.AddCarter();

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });

        services.AddMarten(configure =>
        {
            configure.Connection(builder.Configuration.GetConnectionString("Database")!);
            configure.Schema.For<ShoppingCart>().Identity(x => x.UserName);
        }).UseLightweightSessions();
        
        services.AddScoped<IBasketRepository, BasketRepository>();
        services.Decorate<IBasketRepository, CachedBasketRepository>();
        
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = builder.Configuration.GetConnectionString("Redis")!;
        });
        
        services.AddExceptionHandler<CustomExceptionHandler>();
    }
}
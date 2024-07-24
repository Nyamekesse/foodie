using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain;

namespace Restaurants.Infrastructure;

public static class ServiceCollectionExtension
{
    public static void AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionString = configuration.GetConnectionString("DatabaseConnection");
        services.AddDbContext<RestaurantDbContext>(options =>
            options.UseNpgsql(connectionString).EnableSensitiveDataLogging()
        );
        services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
        services.AddScoped<IRestaurantRepository, RestaurantRepository>();
    }
}

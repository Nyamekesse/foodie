using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Restaurants.Infrastructure;

public static class ServiceCollectionExtension
{
    public static void AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionString = configuration.GetConnectionString("DatabaseConnection");
        services.AddDbContext<RestaurantDbContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
    }
}

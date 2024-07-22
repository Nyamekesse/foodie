using Microsoft.Extensions.DependencyInjection;

namespace Restaurants.Application;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IRestaurantsService, RestaurantService>();
    }
}

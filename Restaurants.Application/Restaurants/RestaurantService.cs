using Microsoft.Extensions.Logging;
using Restaurants.Domain;

namespace Restaurants.Application;

internal class RestaurantService(
    IRestaurantRepository restaurantRepository,
    ILogger<RestaurantService> logger
) : IRestaurantsService
{
    public async Task<IEnumerable<Restaurant>> GetAllRestaurants()
    {
        logger.LogInformation("Getting all restaurants");
        var restaurants = await restaurantRepository.GetAllAsync();
        return restaurants;
    }
}

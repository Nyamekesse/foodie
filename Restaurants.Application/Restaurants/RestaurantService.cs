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

    public async Task<Restaurant?> GetById(int id)
    {
        logger.LogInformation($"Getting restaurant with {id}");
        var restaurant = await restaurantRepository.GetByIdAsync(id);
        return restaurant;
    }
}

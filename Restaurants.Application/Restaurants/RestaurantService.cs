using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Domain;

namespace Restaurants.Application;

internal class RestaurantService(
    IRestaurantRepository restaurantRepository,
    ILogger<RestaurantService> logger,
    IMapper mapper
) : IRestaurantsService
{
    public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
    {
        logger.LogInformation("Getting all restaurants");
        var restaurants = await restaurantRepository.GetAllAsync();
        // var restaurantDto = restaurants.Select(RestaurantDto.FromRestaurant);
        var restaurantDto = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
        return restaurantDto;
    }

    public async Task<RestaurantDto?> GetById(int id)
    {
        logger.LogInformation($"Getting restaurant with {id}");
        var restaurant = await restaurantRepository.GetByIdAsync(id);
        // var restaurantDto = RestaurantDto.FromRestaurant(restaurant);
        var restaurantDto = mapper.Map<RestaurantDto?>(restaurant);
        return restaurantDto;
    }
}

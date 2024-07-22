using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain;

namespace Restaurants.Application;

internal class RestaurantService(
    IRestaurantRepository restaurantRepository,
    ILogger<RestaurantService> logger,
    IMapper mapper
) : IRestaurantsService
{
    public async Task<int> Create(CreateRestaurantDto createRestaurantDto)
    {
        logger.LogInformation("Getting all restaurants");
        var restaurant = mapper.Map<Restaurant>(createRestaurantDto);
        int id = await restaurantRepository.Create(restaurant);
        return id;
    }

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

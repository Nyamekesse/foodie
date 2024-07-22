using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain;

namespace Restaurants.Application;

public interface IRestaurantsService
{
    Task<IEnumerable<RestaurantDto>> GetAllRestaurants();
    Task<RestaurantDto?> GetById(int id);

    Task<int> Create(CreateRestaurantDto createRestaurantDto);
}

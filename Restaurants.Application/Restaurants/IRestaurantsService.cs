using Restaurants.Domain;

namespace Restaurants.Application;

public interface IRestaurantsService
{
    public Task<IEnumerable<RestaurantDto>> GetAllRestaurants();
    public Task<RestaurantDto?> GetById(int id);
}

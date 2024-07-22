using Restaurants.Domain;

namespace Restaurants.Application;

public interface IRestaurantsService
{
    public Task<IEnumerable<Restaurant>> GetAllRestaurants();
}

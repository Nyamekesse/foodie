namespace Restaurants.Domain;

public interface IRestaurantRepository
{
    Task<IEnumerable<Restaurant>> GetAllAsync();
}

namespace Restaurants.Domain.Repositories
{
    public interface IDishesRepository
    {
        Task<int> Create(Dish entity);
        Task DeleteDishesForRestaurant(IEnumerable<Dish> entities);
    }
}

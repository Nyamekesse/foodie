using Restaurants.Domain;
using Restaurants.Domain.Repositories;

namespace Restaurants.Infrastructure.Repositories
{
    internal class DishesRepository(RestaurantDbContext _dbContext) : IDishesRepository
    {
        public async Task<int> Create(Dish entity)
        {
            _dbContext.Dishes.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteDishesForRestaurant(IEnumerable<Dish> entities)
        {
            _dbContext.Dishes.RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
        }
    }
}

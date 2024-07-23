using Microsoft.EntityFrameworkCore;
using Restaurants.Domain;

namespace Restaurants.Infrastructure;

internal class RestaurantRepository(RestaurantDbContext _dbContext) : IRestaurantRepository
{
    public async Task<int> Create(Restaurant entity)
    {
        _dbContext.Restaurants.Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        var restaurants = await _dbContext.Restaurants.ToListAsync();
        return restaurants;
    }

    public async Task<Restaurant?> GetByIdAsync(int id)
    {
        var restaurant = await _dbContext
            .Restaurants.Include(r => r.Dishes)
            .FirstOrDefaultAsync(r => r.Id == id);
        return restaurant;
    }

    public async Task Delete(Restaurant entity)
    {
        _dbContext.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public Task SaveChanges() => _dbContext.SaveChangesAsync();
}

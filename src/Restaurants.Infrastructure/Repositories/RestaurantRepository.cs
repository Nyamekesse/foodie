using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain;
using Restaurants.Domain.Constants;

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

    public async Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(
        string? searchPhrase,
        int pageSize,
        int pageNumber,
        string? sortBy,
        SortDirection sortDirection
    )
    {
        var searchPhraseLower = searchPhrase?.ToLower();
        var baseQuery = _dbContext.Restaurants.Where(restaurant =>
            searchPhrase == null
            || restaurant.Name.ToLower().Contains(searchPhraseLower)
            || restaurant.Description.ToLower().Contains(searchPhraseLower)
        );
        var totalCount = await baseQuery.CountAsync();
        if (sortBy is not null)
        {
            var columnSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
            {
                { nameof(Restaurant.Name), r => r.Name },
                { nameof(Restaurant.Description), r => r.Description },
                { nameof(Restaurant.Category), r => r.Category }
            };

            var selectedColumn = columnSelector[sortBy];
            baseQuery =
                sortDirection == SortDirection.Ascending
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
        }
        var restaurants = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

        return (restaurants, totalCount);
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

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
}

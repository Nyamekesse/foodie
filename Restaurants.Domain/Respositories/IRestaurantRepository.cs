﻿namespace Restaurants.Domain;

public interface IRestaurantRepository
{
    Task<IEnumerable<Restaurant>> GetAllAsync();
    Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(
        string? searchPhrase,
        int pageSize,
        int pageNumber
    );
    Task<Restaurant?> GetByIdAsync(int id);
    Task<int> Create(Restaurant restaurant);
    Task Delete(Restaurant restaurant);
    Task SaveChanges();
}

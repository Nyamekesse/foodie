using Microsoft.EntityFrameworkCore;
using Restaurants.Domain;

namespace Restaurants.Infrastructure;

internal class RestaurantDbContext(DbContextOptions<RestaurantDbContext> options)
    : DbContext(options)
{
    internal DbSet<Restaurant> Restaurants { get; set; }
    internal DbSet<Dish> Dishes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Restaurant>().OwnsOne(r => r.Address);
        modelBuilder
            .Entity<Restaurant>()
            .HasMany(r => r.Dishes)
            .WithOne()
            .HasForeignKey(d => d.RestaurantId);
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Infrastructure.Authorization.Requirements;
using Restaurants.Infrastructure.Repositories;

namespace Restaurants.Infrastructure;

public static class ServiceCollectionExtension
{
    public static void AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionString = configuration.GetConnectionString("DatabaseConnection");
        services.AddDbContext<RestaurantDbContext>(options =>
            options.UseNpgsql(connectionString).EnableSensitiveDataLogging()
        );
        services
            .AddIdentityApiEndpoints<User>()
            .AddRoles<IdentityRole>()
            .AddClaimsPrincipalFactory<RestaurantUserClaimsPrincipalFactory>()
            .AddEntityFrameworkStores<RestaurantDbContext>();
        services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
        services.AddScoped<IRestaurantRepository, RestaurantRepository>();
        services.AddScoped<IDishesRepository, DishesRepository>();
        services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
        services
            .AddAuthorizationBuilder()
            .AddPolicy(
                PolicyNames.HasNationality,
                builder => builder.RequireClaim(AppClaimTypes.Nationality, "Ghanaian", "Polish")
            )
            .AddPolicy(
                PolicyNames.AtLeast20YearsOld,
                builder => builder.AddRequirements(new MinimumAgeRequirement(20))
            );
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain;

namespace Restaurants.Infrastructure.Authorization.Requirements
{
    public class CreatedMultipleRestaurantsRequirementHandler(
        IRestaurantRepository restaurantRepository,
        IUserContext userContext,
        ILogger<CreatedMultipleRestaurantsRequirementHandler> _logger
    ) : AuthorizationHandler<CreatedMultipleRestaurantsRequirement>
    {
        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            CreatedMultipleRestaurantsRequirement requirement
        )
        {
            var currentUser = userContext.GetCurrentUser();
            if (currentUser is null)
            {
                _logger.LogInformation("User is null");
                context.Fail();
            }
            var restaurants = await restaurantRepository.GetAllAsync();
            int userRestaurantsCount = restaurants.Count(restaurant =>
                restaurant.OwnerId == currentUser!.Id
            );
            if (userRestaurantsCount >= requirement.MinimumRestaurantsCreated)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
    }
}

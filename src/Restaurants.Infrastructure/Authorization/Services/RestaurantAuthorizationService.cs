using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Infrastructure.Authorization.Services
{
    public class RestaurantAuthorizationService(
        ILogger<RestaurantAuthorizationService> logger,
        IUserContext userContext
    ) : IRestaurantAuthorizationService
    {
        public bool Authorize(Restaurant restaurant, ResourceOperation resourceOperation)
        {
            var user = userContext.GetCurrentUser();
            logger.LogInformation(
                "Authorizing user {UserEmail} to {Operation} for {RestaurantName}",
                user.Email,
                resourceOperation,
                restaurant.Name
            );

            if (
                resourceOperation == ResourceOperation.Read
                || resourceOperation == ResourceOperation.Create
            )
            {
                logger.LogInformation("Create/read operation - successful authorization");
                return true;
            }

            if (resourceOperation == ResourceOperation.Delete && user.IsInRole(UserRoles.Admin))
            {
                logger.LogInformation("Admin user, delete operation - successful authorization");
                return true;
            }

            if (
                (
                    resourceOperation == ResourceOperation.Delete
                    || resourceOperation == ResourceOperation.Update
                )
                && restaurant.OwnerId == user.Id
            )
            {
                logger.LogInformation(
                    "Owner user, delete/update operation - successful authorization"
                );
                return true;
            }

            return false;
        }
    }
}

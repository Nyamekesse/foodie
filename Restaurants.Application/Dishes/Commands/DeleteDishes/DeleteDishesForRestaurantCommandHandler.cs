using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.DeleteDishes
{
    public class DeleteDishesForRestaurantCommandHandler(
        ILogger<DeleteDishesForRestaurantCommandHandler> _logger,
        IRestaurantRepository _restaurantRepository,
        IDishesRepository _dishesRepository,
        IRestaurantAuthorizationService restaurantAuthorizationService
    ) : IRequestHandler<DeleteDishesForRestaurantCommand>
    {
        public async Task Handle(
            DeleteDishesForRestaurantCommand request,
            CancellationToken cancellationToken
        )
        {
            _logger.LogWarning(
                "Removing all dishes for restaurant {RestaurantId}",
                request.RestaurantId
            );
            var restaurant = await _restaurantRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant is null)
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
                throw new ForbidException();
            await _dishesRepository.DeleteDishesForRestaurant(restaurant.Dishes);
        }
    }
}

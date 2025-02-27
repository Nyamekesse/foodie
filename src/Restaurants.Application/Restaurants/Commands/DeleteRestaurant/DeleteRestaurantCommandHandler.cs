using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant
{
    public class DeleteRestaurantCommandHandler(
        ILogger<DeleteRestaurantCommandHandler> logger,
        IRestaurantRepository restaurantRepository,
        IRestaurantAuthorizationService restaurantAuthorizationService
    ) : IRequestHandler<DeleteRestaurantCommand>
    {
        public async Task Handle(
            DeleteRestaurantCommand request,
            CancellationToken cancellationToken
        )
        {
            logger.LogInformation("Deleting restaurant with ID {RestaurantId}", request.Id);
            var restaurant = await restaurantRepository.GetByIdAsync(request.Id);
            if (restaurant is null)
                throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

            if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Delete))
                throw new ForbidException();

            await restaurantRepository.Delete(restaurant);
        }
    }
}

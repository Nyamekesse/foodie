using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant
{
    public class DeleteRestaurantCommandHandler(
        ILogger<DeleteRestaurantCommandHandler> logger,
        IRestaurantRepository restaurantRepository
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

            await restaurantRepository.Delete(restaurant);
        }
    }
}

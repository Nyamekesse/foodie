using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Dishes.Queries.GetDishesForRestaurant
{
    public class GetDishesForRestaurantQueryHandler(
        ILogger<GetDishesForRestaurantQueryHandler> _logger,
        IRestaurantRepository _restaurantRepository,
        IMapper mapper
    ) : IRequestHandler<GetDishesForRestaurantQuery, IEnumerable<DishDto>>
    {
        public async Task<IEnumerable<DishDto>> Handle(
            GetDishesForRestaurantQuery request,
            CancellationToken cancellationToken
        )
        {
            _logger.LogInformation(
                "Retrieving dishes for restaurant with id {RestaurantId}",
                request.RestaurantId
            );
            var restaurant = await _restaurantRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant is null)
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            var results = mapper.Map<IEnumerable<DishDto>>(restaurant.Dishes);
            return results;
        }
    }
}

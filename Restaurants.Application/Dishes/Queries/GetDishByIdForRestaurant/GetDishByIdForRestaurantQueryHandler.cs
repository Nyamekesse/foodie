using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant
{
    public class GetDishByIdForRestaurantQueryHandler(
        ILogger<GetDishByIdForRestaurantQueryHandler> _logger,
        IRestaurantRepository _restaurantRepository,
        IMapper _mapper
    ) : IRequestHandler<GetDishByIdForRestaurantQuery, DishDto>
    {
        public async Task<DishDto> Handle(
            GetDishByIdForRestaurantQuery request,
            CancellationToken cancellationToken
        )
        {
            _logger.LogInformation(
                "Retrieving dish with id {DishId} for restaurant with id {RestaurantId}",
                request.DishId,
                request.RestaurantId
            );
            var restaurant = await _restaurantRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant is null)
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            var dish = restaurant.Dishes.FirstOrDefault(dish => dish.Id == request.DishId);
            if (dish is null)
                throw new NotFoundException(nameof(Dish), request.DishId.ToString());
            var result = _mapper.Map<DishDto>(dish);
            return result;
        }
    }
}

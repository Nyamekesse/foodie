using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.CreateDish
{
    public class CreateDishCommandHandler(
        ILogger<CreateDishCommandHandler> _logger,
        IRestaurantRepository _restaurantRepository,
        IDishesRepository _dishesRepository,
        IMapper _mapper,
        IRestaurantAuthorizationService restaurantAuthorizationService
    ) : IRequestHandler<CreateDishCommand, int>
    {
        public async Task<int> Handle(
            CreateDishCommand request,
            CancellationToken cancellationToken
        )
        {
            _logger.LogInformation("Creating a new dish {@Dish}", request);
            var restaurant = await _restaurantRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant is null)
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
                throw new ForbidException();
            var dish = _mapper.Map<Dish>(request);
            return await _dishesRepository.Create(dish);
        }
    }
}

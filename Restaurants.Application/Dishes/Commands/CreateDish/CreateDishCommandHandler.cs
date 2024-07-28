using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.CreateDish
{
    public class CreateDishCommandHandler(
        ILogger<CreateDishCommandHandler> _logger,
        IRestaurantRepository _restaurantRepository,
        IDishesRepository _dishesRepository,
        IMapper _mapper
    ) : IRequestHandler<CreateDishCommand>
    {
        public async Task Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating a new dish {@Dish}", request);
            var restaurant = await _restaurantRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant is null)
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            var dish = _mapper.Map<Dish>(request);
            await _dishesRepository.Create(dish);
        }
    }
}

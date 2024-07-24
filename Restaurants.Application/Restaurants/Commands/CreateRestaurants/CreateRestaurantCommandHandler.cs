using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurants
{
    public class CreateRestaurantCommandHandler(
        IRestaurantRepository restaurantRepository,
        ILogger<CreateRestaurantCommandHandler> logger,
        IMapper mapper
    ) : IRequestHandler<CreateRestaurantCommand, int>
    {
        public async Task<int> Handle(
            CreateRestaurantCommand request,
            CancellationToken cancellationToken
        )
        {
            logger.LogInformation("Creating a new restaurant {@Restaurant}", request);
            var restaurant = mapper.Map<Restaurant>(request);
            int id = await restaurantRepository.Create(restaurant);
            return id;
        }
    }
}

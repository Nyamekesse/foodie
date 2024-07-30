using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurants
{
    public class CreateRestaurantCommandHandler(
        IRestaurantRepository restaurantRepository,
        ILogger<CreateRestaurantCommandHandler> logger,
        IMapper mapper,
        IUserContext _userContext
    ) : IRequestHandler<CreateRestaurantCommand, int>
    {
        public async Task<int> Handle(
            CreateRestaurantCommand request,
            CancellationToken cancellationToken
        )
        {
            var currentUser = _userContext.GetCurrentUser();
            logger.LogInformation(
                "{UserEmail} with id: {UserId} is creating a new restaurant {@Restaurant}",
                currentUser!.Email,
                currentUser!.Id,
                request
            );
            var restaurant = mapper.Map<Restaurant>(request);
            restaurant.OwnerId = currentUser.Id;
            int id = await restaurantRepository.Create(restaurant);
            return id;
        }
    }
}

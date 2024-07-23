using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById
{
    public class GetRestaurantByIdQueryHandler(
        IRestaurantRepository restaurantRepository,
        ILogger<GetRestaurantByIdQueryHandler> logger,
        IMapper mapper
    ) : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto?>
    {
        public async Task<RestaurantDto?> Handle(
            GetRestaurantByIdQuery request,
            CancellationToken cancellationToken
        )
        {
            logger.LogInformation($"Getting restaurant with {request.Id}");
            var restaurant = await restaurantRepository.GetByIdAsync(request.Id);
            var restaurantDto = mapper.Map<RestaurantDto?>(restaurant);
            return restaurantDto;
        }
    }
}

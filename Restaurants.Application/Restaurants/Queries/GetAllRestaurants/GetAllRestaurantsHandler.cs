using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsHandler(
        IRestaurantRepository restaurantRepository,
        ILogger<GetAllRestaurantsHandler> logger,
        IMapper mapper
    ) : IRequestHandler<GetAllRestaurantQuery, IEnumerable<RestaurantDto>>
    {
        public async Task<IEnumerable<RestaurantDto>> Handle(
            GetAllRestaurantQuery request,
            CancellationToken cancellationToken
        )
        {
            logger.LogInformation("Getting all restaurants");
            var restaurants = await restaurantRepository.GetAllAsync();
            var restaurantDto = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
            return restaurantDto;
        }
    }
}

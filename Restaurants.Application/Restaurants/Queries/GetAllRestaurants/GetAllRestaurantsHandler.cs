using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common;
using Restaurants.Domain;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsHandler(
        IRestaurantRepository restaurantRepository,
        ILogger<GetAllRestaurantsHandler> logger,
        IMapper mapper
    ) : IRequestHandler<GetAllRestaurantQuery, PagedResult<RestaurantDto>>
    {
        public async Task<PagedResult<RestaurantDto>> Handle(
            GetAllRestaurantQuery request,
            CancellationToken cancellationToken
        )
        {
            logger.LogInformation("Getting all restaurants");
            var (restaurants, totalCount) = await restaurantRepository.GetAllMatchingAsync(
                request.SearchPhrase,
                request.PageSize,
                request.PageNumber,
                request.SortBy,
                request.SortDirection
            );
            var restaurantDtos = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
            var result = new PagedResult<RestaurantDto>(
                restaurantDtos,
                totalCount,
                request.PageSize,
                request.PageNumber
            );
            return result;
        }
    }
}

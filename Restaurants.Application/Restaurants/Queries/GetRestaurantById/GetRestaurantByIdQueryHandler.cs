using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById
{
    public class GetRestaurantByIdQueryHandler(
        IRestaurantRepository restaurantRepository,
        ILogger<GetRestaurantByIdQueryHandler> logger,
        IMapper mapper
    ) : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto?>
    {
        public async Task<RestaurantDto> Handle(
            GetRestaurantByIdQuery request,
            CancellationToken cancellationToken
        )
        {
            logger.LogInformation("Retrieving restaurant with Id {RestaurantId}", request.Id);
            var restaurant =
                await restaurantRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
            ;
            var restaurantDto = mapper.Map<RestaurantDto>(restaurant);
            return restaurantDto;
        }
    }
}

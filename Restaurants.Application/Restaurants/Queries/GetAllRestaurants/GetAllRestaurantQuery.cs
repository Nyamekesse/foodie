using MediatR;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantQuery : IRequest<IEnumerable<RestaurantDto>> { }
}

using MediatR;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById
{
    public class GetRestaurantByIdQuery(int id) : IRequest<RestaurantDto?>
    {
        public int Id { get; } = id;
    }
}

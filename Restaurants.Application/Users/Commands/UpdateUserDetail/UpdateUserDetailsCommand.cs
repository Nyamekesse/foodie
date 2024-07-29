using MediatR;

namespace Restaurants.Application.Users.Commands.UpdateUserDetail
{
    public class UpdateUserDetailsCommand : IRequest
    {
        public DateOnly? DateOfBirth { get; set; }
        public string? Nationality { get; set; }
    }
}

using FluentValidation;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantQuery>
    {
        private readonly int[] allowedPageSIzes = [5, 10, 15, 20, 25, 30];

        public GetAllRestaurantsQueryValidator()
        {
            RuleFor(restaurant => restaurant.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(restaurant => restaurant.PageSize)
                .Must(value => allowedPageSIzes.Contains(value))
                .WithMessage($"Page size must be in {string.Join(", ", allowedPageSIzes)}");
        }
    }
}

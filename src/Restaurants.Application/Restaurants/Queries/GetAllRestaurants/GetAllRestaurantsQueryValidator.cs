using FluentValidation;
using Restaurants.Domain;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantQuery>
    {
        private readonly int[] allowedPageSIzes = [5, 10, 15, 20, 25, 30];
        private string[] allowedSortByColumns =
        [
            nameof(RestaurantDto.Name),
            nameof(RestaurantDto.Category),
            nameof(RestaurantDto.Description)
        ];

        public GetAllRestaurantsQueryValidator()
        {
            RuleFor(restaurant => restaurant.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(restaurant => restaurant.PageSize)
                .Must(value => allowedPageSIzes.Contains(value))
                .WithMessage($"Page size must be in {string.Join(", ", allowedPageSIzes)}");
            RuleFor(restaurant => restaurant.SortBy)
                .Must(value => allowedSortByColumns.Contains(value))
                .When(restaurant => restaurant.SortBy is not null)
                .WithMessage(
                    $"Sort by is optional or the column must be in {string.Join(", ", allowedSortByColumns)}"
                );
        }
    }
}

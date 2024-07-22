using System.Data;
using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Validators
{
    public class CreateRestaurantDtoValidator : AbstractValidator<CreateRestaurantDto>
    {
        private readonly List<string> validCategories = ["Fast Food", "Cafe", "Bar"];

        public CreateRestaurantDtoValidator()
        {
            RuleFor(dto => dto.Name).Length(3, 100);
            RuleFor(dto => dto.Category)
                .Must(validCategories.Contains)
                .WithMessage("Category is not valid");
            // .Custom(
            //     (value, context) =>
            //     {
            //         var isValidCategory = validCategories.Contains(value);
            //         if (!isValidCategory)
            //             context.AddFailure("Category", "Category is not valid");
            //     }
            // );
            RuleFor(dto => dto.Description)
                .NotEmpty()
                .WithMessage("Description is required");
            RuleFor(dto => dto.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(dto => dto.ContactEmail)
                .EmailAddress()
                .WithMessage("Contact email is not valid");
            RuleFor(dto => dto.PostalCode)
                .Matches(@"^\d{2}-\d{3}$")
                .WithMessage("Postal code is not valid");
        }
    }
}

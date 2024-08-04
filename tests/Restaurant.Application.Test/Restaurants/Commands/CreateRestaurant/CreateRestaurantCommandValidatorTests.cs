using System.Runtime.InteropServices;
using FluentValidation.TestHelper;
using Restaurants.Application.Restaurants.Commands.CreateRestaurants;
using Restaurants.Application.Restaurants.Validators;
using Xunit;

namespace Restaurant.Application.Test.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandValidatorTests
    {
        [Fact]
        public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
        {
            // Arrange
            var command = new CreateRestaurantCommand()
            {
                Name = "Test",
                Category = "Fast Food",
                Description = "Test Description",
                ContactEmail = "test@test.com",
                PostalCode = "12-123"
            };
            var validator = new CreateRestaurantCommandValidator();

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Validator_ForInvalidCommand_ShouldHaveValidationErrors()
        {
            // Arrange
            var command = new CreateRestaurantCommand()
            {
                Name = "Tt",
                Category = "Fast Fast Food",
                Description = "",
                ContactEmail = "test.com",
                PostalCode = "127-123"
            };
            var validator = new CreateRestaurantCommandValidator();

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
            result.ShouldHaveValidationErrorFor(x => x.Category);
            result.ShouldHaveValidationErrorFor(x => x.Description);
            result.ShouldHaveValidationErrorFor(x => x.ContactEmail);
            result.ShouldHaveValidationErrorFor(x => x.PostalCode);
        }

        [Theory()]
        [InlineData("Fast Food")]
        [InlineData("Cafe")]
        [InlineData("Bar")]
        public void Validator_ForValidCategory_ShouldNotHaveValidationErrorsForCategoryProperty(
            string category
        )
        {
            // Arrange
            var validator = new CreateRestaurantCommandValidator();
            var command = new CreateRestaurantCommand { Category = category };

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Category);
        }

        [Theory()]
        [InlineData("10220")]
        [InlineData("102-56")]
        [InlineData("10 123")]
        [InlineData("10-1 23")]
        public void Validator_ForInValidPostalCode_ShouldHaveValidationErrorsForPostalCodeProperty(
            string postalCode
        )
        {
            // Arrange
            var validator = new CreateRestaurantCommandValidator();
            var command = new CreateRestaurantCommand { PostalCode = postalCode };

            // Act
            var result = validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.PostalCode);
        }
    }
}

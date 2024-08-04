using System.Security.Claims;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using Xunit;

namespace Restaurant.Application.Test.Users
{
    public class GetCurrentUserTests
    {
        [Fact]
        public void GetCurrentUser_WithAuthenticatedUser_ShouldReturnCurrentUser()
        {
            // Arrange
            var dateOfBirth = new DateOnly(1998, 1, 1);
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            var claims = new List<Claim>()
            {
                new(ClaimTypes.NameIdentifier, "1"),
                new(ClaimTypes.Email, "test@test"),
                new(ClaimTypes.Role, UserRoles.Admin),
                new(ClaimTypes.Role, UserRoles.User),
                new("Nationality", "Ghanaian"),
                new("DateOfBirth", dateOfBirth.ToString("yyyy-MM-dd"))
            };
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));
            httpContextAccessor
                .Setup(x => x.HttpContext)
                .Returns(new DefaultHttpContext() { User = user });
            var userContext = new UserContext(httpContextAccessor.Object);

            // Act
            var currentUser = userContext.GetCurrentUser();

            // Assert
            currentUser.Should().NotBeNull();
            currentUser!.Id.Should().Be("1");
            currentUser.Email.Should().Be("test@test");
            currentUser.Roles.Should().ContainInOrder(UserRoles.Admin, UserRoles.User);
            currentUser.Nationality.Should().Be("Ghanaian");
            currentUser.DateOfBirth.Should().Be(dateOfBirth);
        }

        [Fact]
        public void GetCurrentUser_WithUserContextNotPresent_ThrowsInvalidOperationException()
        {
            // Arrange
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            httpContextAccessor.Setup(x => x.HttpContext).Returns((HttpContext?)null);
            var userContext = new UserContext(httpContextAccessor.Object);

            // Act
            Action action = () => userContext.GetCurrentUser();

            // Assert
            action
                .Should()
                .Throw<InvalidOperationException>()
                .WithMessage("User context is not present");
        }
    }
}

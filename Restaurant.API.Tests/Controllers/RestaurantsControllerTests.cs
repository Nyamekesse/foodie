using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Restaurants.Application;
using Restaurants.Domain;
using Xunit;

namespace Restaurant.API.Tests.Controllers
{
    public class RestaurantsControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly Mock<IRestaurantRepository> _restaurantRepositoryMock = new();

        public RestaurantsControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                    // services.Replace(
                    //     ServiceDescriptor.Scoped(
                    //         typeof(IRestaurantRepository),
                    //         _ => _restaurantRepositoryMock.Object
                    //     )
                    // );
                });
            });
        }

        [Fact]
        public async Task GetAll_ForValidRequest_ShouldReturn200Ok()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var result = await client.GetAsync("/api/restaurants?pageSize=5&pageNumber=1");

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetAll_ForInvalidRequest_ShouldReturn400BadRequest()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/restaurants");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetById_ForNoneExistingId_ShouldReturn404NotFound()
        {
            // Arrange
            var id = 566;
            // _restaurantRepositoryMock.Setup(mock => mock.GetByIdAsync(id)).ReturnsAsync((Restaurant?)null);
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync($"/api/restaurants/{id}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetById_ForExistingId_ShouldReturn200Ok()
        {
            // Arrange
            var id = 3;
            // _restaurantRepositoryMock.Setup(mock => mock.GetByIdAsync(id)).ReturnsAsync((Restaurant?)null);

            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync($"/api/restaurants/{id}");
            var restaurantDto = await response.Content.ReadFromJsonAsync<RestaurantDto>();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            restaurantDto.Should().NotBeNull();
        }
    }
}

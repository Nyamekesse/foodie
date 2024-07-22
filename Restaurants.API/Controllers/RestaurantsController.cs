using Microsoft.AspNetCore.Mvc;
using Restaurants.Application;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantsController(IRestaurantsService restaurantsService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var restaurants = await restaurantsService.GetAllRestaurants();
            return Ok(restaurants);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var restaurant = await restaurantsService.GetById(id);
            if (restaurant is null)
                return NotFound();
            return Ok(restaurant);
        }
    }
}

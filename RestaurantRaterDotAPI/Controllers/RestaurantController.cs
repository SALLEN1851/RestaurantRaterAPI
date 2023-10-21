using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;   // for List<>
using System.Threading.Tasks;       // for async and Task<>
using Microsoft.EntityFrameworkCore; // for Entity Framework methods like ToListAsync()

namespace RestaurantRaterDotAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantController : ControllerBase 
    {
        private readonly RestaurantDbContext _context;

        public RestaurantController(RestaurantDbContext context)
        {
            _context = context;
        }

        // Async GET Endpoint
        [HttpGet]
        public async Task<IActionResult> GetRestaurants()
        {
            List<Restaurant> restaurants = await _context.Restaurants.ToListAsync();
            return Ok(restaurants);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetRestaurantById(int id)
        {
            Restaurant? restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant is null)
            {
                return NotFound();
            }
            return Ok(restaurant);
        }

        // Async POST Endpoint
        [HttpPost]
        public async Task<IActionResult> PostRestaurant(Restaurant request) // I assumed you need an input parameter of type 'Restaurant' for this endpoint
        {
            if (ModelState.IsValid)
            {
                _context.Restaurants.Add(request);
                await _context.SaveChangesAsync();
                return Ok();
            }

            return BadRequest(ModelState);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;   
using System.Threading.Tasks;       
using Microsoft.EntityFrameworkCore; 
using RestaurantRaterDotAPI.Data;

namespace RestaurantRaterDotAPI.Models
{
    public class RatingRequest
    {
        public float Score { get; set; }
        public int RestaurantId { get; set; }
    }
}
namespace RestaurantRaterDotAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantController : ControllerBase 
    {
        public readonly RestaurantDbContext _context;

        public RestaurantController(RestaurantDbContext context)
        {
            _context = context;
        }

        // Async GET Endpoint
        [HttpGet]
        public async Task<IActionResult> GetAllRestaurants()
        {
            var restaurants = await _context.Restaurants.Include(r => r.Ratings).ToListAsync();
            List<RestaurantListItem> restaurantList = restaurants.Select(r => new RestaurantListItem() {
                Id = r.Id,
                Name = r.Name,
                Location = r.Location,
                // AverageScore = r.AverageScore,
            }).ToList();
            return Ok(restaurantList);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetRestaurantById(int id)
        {
            var restaurant = await _context.Restaurants.Include(r => r.Ratings).FirstOrDefaultAsync(r => r.Id == id);
            if (restaurant is null)
            {
                return NotFound();
            }
            return Ok(restaurant);
        }

        // Async POST Endpoint
        [HttpPost]
        public async Task<IActionResult> PostRestaurant(Restaurant request)
        {
            if (ModelState.IsValid)
            {
                _context.Restaurants.Add(request);
                await _context.SaveChangesAsync();
                return Ok();
            }

            return BadRequest(ModelState);
        }


        // Async PUT Endpoint
        [HttpPut]
        public async Task<IActionResult> PutRestaurant([FromBody] Restaurant request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Restaurant? restaurant = await _context.Restaurants.FindAsync(request.Id);
            if (restaurant is null)
            {
                return NotFound();
            }

            restaurant.Name = request.Name;
            restaurant.Location = request.Location;

            _context.Restaurants.Update(restaurant);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // Async DELETE Endpoint
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant is null)
            {
                return NotFound();
            }

            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();
            return Ok();
        }
      }
    }
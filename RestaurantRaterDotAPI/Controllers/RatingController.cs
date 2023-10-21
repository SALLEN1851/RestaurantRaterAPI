using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;  
using System.Threading.Tasks;       
using Microsoft.EntityFrameworkCore;
using RestaurantRaterDotAPI.Data;
using Microsoft.Extensions.Logging;
using System.Linq;
using RestaurantRaterDotAPI.Models;


namespace RestaurantRaterDotAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RatingController : Controller
    {
        private readonly ILogger<RatingController> _logger;
        private RestaurantDbContext _context;

        public RatingController(ILogger<RatingController> logger, RestaurantDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> RateRestaurant([FromForm] RatingEdit model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Ratings.Add(new Rating() {
                Score = model.Score,
                RestaurantId = model.RestaurantId,
            });

            await _context.SaveChangesAsync();
            
            return Ok();
        }
    }
}

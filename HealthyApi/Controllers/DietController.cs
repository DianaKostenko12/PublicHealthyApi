using HealthyApi.Data;
using HealthyApi.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HealthyApi.Controllers
{
    [ApiController]
    [Route("Diet")]
    public class DietController : ControllerBase
    {
        private readonly DataContext _context;
        public DietController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("diet")]
        public async Task<IActionResult> AddFavoriteDiet(Favorite model)
        {
            _context.Favorites.Add(model);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("diet/{userId}")]
        public async Task<ActionResult<List<string>>> GetAllDietById(long userid)
        {
            var diets = _context.Favorites
                .Where(x => x.UserId == userid)
                .Select(x => x.FavoriteDiet)
                .ToList();

            if (diets == null || diets.Count == 0)
            {
                return BadRequest("You doesn't have any favorite diet");
            }

            return Ok(diets);
        }

        [HttpDelete("{userid}")]
        public async Task<IActionResult> AutoDeleteFavouriteDiet(long userId)
        {
            var diet = _context.Favorites
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.Id)
                .FirstOrDefault();

            if (diet == null)
            {
                return BadRequest("You doesn't have any favorite diet");
            }

            _context.Favorites.Remove(diet);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{userid}/{dietIndex}")]
        public async Task<IActionResult> DeleteFavouriteDiet(long userId, int dietIndex)
        {
            var diet = _context.Favorites
                .Where(x => x.UserId == userId)
                .Skip(dietIndex - 1)
                .FirstOrDefault();

            if (diet == null)
            {
                return BadRequest("You doesn't have any favorite diet");
            }

            _context.Favorites.Remove(diet);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}

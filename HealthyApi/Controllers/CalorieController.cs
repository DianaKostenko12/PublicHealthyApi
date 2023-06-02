using HealthyApi.Data;
using HealthyApi.Services.CalorieServices;
using Microsoft.AspNetCore.Mvc;

namespace HealthyApi.Controllers
{
    [ApiController]
    [Route("Calorie")]
    public class CalorieController : ControllerBase
    {
        private readonly ICalorieService _calorieService;
        private readonly DataContext _context;

        public CalorieController(ICalorieService calorieService, DataContext context)
        {
            _calorieService = calorieService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCaloriesByFood([FromQuery] string food)
        {
            var result = await _calorieService.GetCaloriesByFoodAsync(food);

            return Ok(result);
        }

        [HttpGet("ration/{id}")]
        public async Task<ActionResult<string>> GetDietForAWeekByUserId(long id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return BadRequest("You are not registered!");
            }

            return Ok(await _calorieService.GetDietForAWeekAsync(user));
        }

        [HttpGet("count/{id}")]
        public async Task<IActionResult> GetCaloriesADayByUserId(long id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return BadRequest("You are not registered!");
            }

            return Ok(await _calorieService.GetCaloriesADayAsync(user));
        }

        [HttpGet("advice")]
        public async Task<IActionResult> GetAdviceAboutRation([FromQuery] string ration)
        {
            var result = await _calorieService.GetAdviceAboutRationAsync(ration);

            return Ok(result);
        }
    }
}

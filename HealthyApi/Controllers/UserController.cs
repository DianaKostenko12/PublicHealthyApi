using HealthyApi.Data;
using HealthyApi.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using ImageChartsLib;

namespace HealthyApi.Controllers
{
    [ApiController]
    [Route("Auth")]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        public UserController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(User model)
        {
            var user = _context.Users.Find(model.Id);

            if (user != null)
            {
                return BadRequest("User has already registered");
            }

            _context.Users.Add(model);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<bool> IsUserRegistered(long id)
        {
            var user = _context.Users.Find(id);

            return user != null;
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser(User model)
        {
            var user = _context.Users.Find(model.Id);

            if (user == null)
            {
                return BadRequest("User has not been registered");
            }

            user.Sex = model.Sex;
            user.Weight = model.Weight;
            user.Height = model.Height;
            user.Age = model.Age;

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("weight")]
        public async Task<IActionResult> BuildChartByWeight([FromQuery] string weights)
        {
            string chartUrl = new ImageCharts()
               .cht("ls") // vertical bar chart
               .chs("400x200") // 300px x 300px
               .chd($"a:{weights.Replace(" ", "")}") // 2 data points: 60 and 40
               .toURL(); // get the generated URL

            return Ok(chartUrl);
        }
    }
}

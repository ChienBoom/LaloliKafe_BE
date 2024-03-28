using LoveKafe_BE.Auth;
using LoveKafe_BE.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoveKafe_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AreaController : Controller
    {
        private AppDbContext _context;
        public AreaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Area> areas = _context.Area.ToList();
            return Ok(new ResultData<Area> { TotalCount = areas.Count, Data = areas });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            Area area = _context.Area.FirstOrDefault(c => c.Id == id);
            if (area == null) return NotFound();
            return Ok(area);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Area value)
        {
            _context.Area.Add(value);
            _context.SaveChanges();
            return Ok(value);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Area value)
        {
            Area area = _context.Area.FirstOrDefault(o => o.Id == id);
            if (area == null) return NotFound();
            _context.Entry(area).CurrentValues.SetValues(value);
            _context.SaveChangesAsync();
            return Ok(value);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            Area area = _context.Area.FirstOrDefault(o => o.Id == id);
            if (area == null) return NotFound();
            _context.Area.Remove(area);
            _context.SaveChangesAsync();
            return Ok();
        }
    }
}

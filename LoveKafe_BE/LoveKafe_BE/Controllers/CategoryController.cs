using LoveKafe_BE.Auth;
using LoveKafe_BE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace LoveKafe_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CategoryController> _logger;
        public CategoryController(AppDbContext context, ILogger<CategoryController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Category> categories = _context.Category.ToList();
            _logger.LogInformation("LoveKafe getCategory");
            _logger.LogWarning("This is a warning log message from HomeController.");
            _logger.LogError("This is an error log message from HomeController.");
            Log.Information("LoveKafe getCategory");
            return Ok(new ResultData<Category> { TotalCount =  categories.Count, Data = categories });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            Category category = _context.Category.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        //[Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] Category value)
        {
            _context.Category.Add(value);
            _context.SaveChanges();
            return Ok(value);
        }

        //[Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Category value)
        {
            Category category = _context.Category.FirstOrDefault(o => o.Id == id);
            if(category == null) return NotFound();
            _context.Entry(category).CurrentValues.SetValues(value);
            _context.SaveChangesAsync();
            return Ok(value);
        }

        //[Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            Category category = _context.Category.FirstOrDefault(o => o.Id == id);
            if (category == null) return NotFound();
            _context.Category.Remove(category);
            _context.SaveChangesAsync();
            return Ok();
        }

    }
}

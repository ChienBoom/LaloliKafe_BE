using LoveKafe_BE.Auth;
using LoveKafe_BE.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoveKafe_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Category> categories = _context.Category.ToList();
            return Ok(new ResultData<Category> { TotalCount =  categories.Count, Data = categories });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            Category category = _context.Category.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Category value)
        {
            _context.Category.Add(value);
            _context.SaveChanges();
            return Ok(value);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Category value)
        {
            Category category = _context.Category.FirstOrDefault(o => o.Id == id);
            if(category == null) return NotFound();
            _context.Entry(category).CurrentValues.SetValues(value);
            _context.SaveChangesAsync();
            return Ok(value);
        }

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

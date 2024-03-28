using LoveKafe_BE.Auth;
using LoveKafe_BE.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoveKafe_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Product> products = _context.Product.ToList();
            return Ok(new ResultData<Product> { TotalCount = products.Count, Data = products });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            Product product = _context.Product.FirstOrDefault(c => c.Id == id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Product value)
        {
            _context.Product.Add(value);
            _context.SaveChanges();
            return Ok(value);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Product value)
        {
            Product product = _context.Product.FirstOrDefault(o => o.Id == id);
            if (product == null) return NotFound();
            _context.Entry(product).CurrentValues.SetValues(value);
            _context.SaveChangesAsync();
            return Ok(value);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            Product product = _context.Product.FirstOrDefault(o => o.Id == id);
            if (product == null) return NotFound();
            _context.Product.Remove(product);
            _context.SaveChangesAsync();
            return Ok();
        }
    }
}

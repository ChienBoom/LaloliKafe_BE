using LoveKafe_BE.Auth;
using LoveKafe_BE.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoveKafe_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private AppDbContext _context;
        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Order> orders = _context.Order.ToList();
            return Ok(new ResultData<Order> { TotalCount = orders.Count, Data = orders });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            Order order = _context.Order.FirstOrDefault(c => c.Id == id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Order value)
        {
            _context.Order.Add(value);
            _context.SaveChanges();
            return Ok(value);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Order value)
        {
            Order order = _context.Order.FirstOrDefault(o => o.Id == id);
            if (order == null) return NotFound();
            _context.Entry(order).CurrentValues.SetValues(value);
            _context.SaveChangesAsync();
            return Ok(value);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            Order order = _context.Order.FirstOrDefault(o => o.Id == id);
            if (order == null) return NotFound();
            _context.Order.Remove(order);
            return Ok();
        }
    }
}

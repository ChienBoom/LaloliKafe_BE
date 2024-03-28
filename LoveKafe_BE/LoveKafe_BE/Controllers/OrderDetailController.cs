using LoveKafe_BE.Auth;
using LoveKafe_BE.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoveKafe_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderDetailController : Controller
    {
        private AppDbContext _context;
        public OrderDetailController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<OrderDetail> orderDetails = _context.OrderDetail.ToList();
            return Ok(new ResultData<OrderDetail> { TotalCount = orderDetails.Count, Data = orderDetails });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            OrderDetail orderDetail = _context.OrderDetail.FirstOrDefault(c => c.Id == id);
            if (orderDetail == null) return NotFound();
            return Ok(orderDetail);
        }

        [HttpPost]
        public IActionResult Post([FromBody] OrderDetail value)
        {
            _context.OrderDetail.Add(value);
            _context.SaveChanges();
            return Ok(value);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] OrderDetail value)
        {
            OrderDetail orderDetail = _context.OrderDetail.FirstOrDefault(o => o.Id == id);
            if (orderDetail == null) return NotFound();
            _context.Entry(orderDetail).CurrentValues.SetValues(value);
            _context.SaveChangesAsync();
            return Ok(value);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            OrderDetail orderDetail = _context.OrderDetail.FirstOrDefault(o => o.Id == id);
            if (orderDetail == null) return NotFound();
            _context.OrderDetail.Remove(orderDetail);
            _context.SaveChangesAsync();
            return Ok();
        }
    }
}

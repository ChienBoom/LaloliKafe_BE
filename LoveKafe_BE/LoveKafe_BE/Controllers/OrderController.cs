using LoveKafe_BE.Auth;
using LoveKafe_BE.Models;
using LoveKafe_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace LoveKafe_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private AppDbContext _context;
        private Util _util;
        public OrderController(AppDbContext context, Util util)
        {
            _context = context;
            _util = util;
        }

        [HttpGet]
        public IActionResult Get()
        {
            string queryString = HttpContext.Request.QueryString.ToString();
            Dictionary<string, string> queryParams = _util.ParseQueryString(queryString);

            Dictionary<string, string> queryModels = new Dictionary<string, string>();

            if (queryParams.TryGetValue("tableId", out string tableId) && tableId != null && tableId != "" && tableId != "undefined") queryModels.Add("tableId", tableId);

            var query = _context.Order.Include(o => o.Table).AsQueryable();
            foreach (var param in queryModels)
            {
                query = query.Where(param.Key + " == @0", param.Value);
            }

            List<Order> orders = query.OrderByDescending(o => o.OrderDate).ToList();
            return Ok(new ResultData<Order> { TotalCount = orders.Count, Data = orders });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            Order order = _context.Order.Include(o => o.Table).FirstOrDefault(c => c.Id == id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        //[Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] Order value)
        {
            _context.Order.Add(value);
            _context.SaveChanges();
            return Ok(value);
        }

        //[Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Order value)
        {
            Order order = _context.Order.FirstOrDefault(o => o.Id == id);
            if (order == null) return NotFound();
            _context.Entry(order).CurrentValues.SetValues(value);
            _context.SaveChangesAsync();
            return Ok(value);
        }

        //[Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            Order order = _context.Order.FirstOrDefault(o => o.Id == id);
            if (order == null) return NotFound();
            _context.Order.Remove(order);
            _context.SaveChangesAsync();
            return Ok();
        }
    }
}

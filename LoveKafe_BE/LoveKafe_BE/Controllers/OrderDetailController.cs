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
    public class OrderDetailController : Controller
    {
        private AppDbContext _context;
        private Util _util;
        public OrderDetailController(AppDbContext context, Util util)
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

            if (queryParams.TryGetValue("orderId", out string orderId) && orderId != null && orderId != "undefined") queryModels.Add("orderId", orderId);

            var query = _context.OrderDetail.Include(o => o.Order).Include(o => o.Product).AsQueryable();
            foreach (var param in queryModels)
            {
                query = query.Where(param.Key + " == @0", param.Value);
            }
            List<OrderDetail> orderDetails = query.ToList();
            return Ok(new ResultData<OrderDetail> { TotalCount = orderDetails.Count, Data = orderDetails });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            OrderDetail orderDetail = _context.OrderDetail.Include(o => o.Order).Include(o => o.Product).FirstOrDefault(c => c.Id == id);
            if (orderDetail == null) return NotFound();
            return Ok(orderDetail);
        }

        //[Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] OrderDetail value)
        {
            _context.OrderDetail.Add(value);
            _context.SaveChanges();
            return Ok(value);
        }

        //[Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] OrderDetail value)
        {
            OrderDetail orderDetail = _context.OrderDetail.FirstOrDefault(o => o.Id == id);
            if (orderDetail == null) return NotFound();
            _context.Entry(orderDetail).CurrentValues.SetValues(value);
            _context.SaveChangesAsync();
            return Ok(value);
        }

        //[Authorize]
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

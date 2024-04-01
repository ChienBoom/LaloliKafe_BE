using LoveKafe_BE.Auth;
using LoveKafe_BE.Models;
using LoveKafe_BE.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace LoveKafe_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private AppDbContext _context;
        private Util _util;
        public ProductController(AppDbContext context, Util util)
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

            if (queryParams.TryGetValue("categoryId", out string categoryId) && categoryId != null && categoryId != "undefined") queryModels.Add("categoryId", categoryId);

            var query = _context.Product.Include(o => o.Category).AsQueryable();
            foreach (var param in queryModels)
            {
                query = query.Where(param.Key + " == @0", param.Value);
            }
            List<Product> products = query.ToList();

            return Ok(new ResultData<Product> { TotalCount = products.Count, Data = products });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            Product product = _context.Product.Include(o => o.Category).FirstOrDefault(c => c.Id == id);
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

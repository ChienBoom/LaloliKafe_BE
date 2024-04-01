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
    public class TableController : Controller
    {
        private AppDbContext _context;
        private Util _util;
        public TableController(AppDbContext context, Util util)
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

            if (queryParams.TryGetValue("areaId", out string areaId) && areaId != null && areaId != "undefined") queryModels.Add("areaId", areaId);

            var query = _context.Table.Include(o => o.Area).AsQueryable();
            foreach (var param in queryModels)
            {
                query = query.Where(param.Key + " == @0", param.Value);
            }
            List<Table> tables = query.ToList();
            return Ok(new ResultData<Table> { TotalCount = tables.Count, Data = tables });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            Table table = _context.Table.Include(o => o.Area).FirstOrDefault(c => c.Id == id);
            if (table == null) return NotFound();
            return Ok(table);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Table value)
        {
            _context.Table.Add(value);
            _context.SaveChanges();
            return Ok(value);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Table value)
        {
            Table table = _context.Table.FirstOrDefault(o => o.Id == id);
            if (table == null) return NotFound();
            _context.Entry(table).CurrentValues.SetValues(value);
            _context.SaveChangesAsync();
            return Ok(value);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            Table table = _context.Table.FirstOrDefault(o => o.Id == id);
            if (table == null) return NotFound();
            _context.Table.Remove(table);
            _context.SaveChangesAsync();
            return Ok();
        }
    }
}

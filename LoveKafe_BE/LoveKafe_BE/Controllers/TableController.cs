using LoveKafe_BE.Auth;
using LoveKafe_BE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoveKafe_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TableController : Controller
    {
        private AppDbContext _context;
        public TableController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Table> tables = _context.Table.Include(o => o.Area).ToList();
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

using LoveKafe_BE.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoveKafe_BE.Controllers
{
    public class BaseController<TEntity, TContext> : ControllerBase
        where TEntity : class
    {
        //    protected readonly AppDbContext _context;
        //    public BaseController(AppDbContext context)
        //    {
        //        _context = context;
        //    }

        //    [HttpGet]
        //    public virtual async Task<ActionResult<IEnumerable<TEntity>>> GetAll()
        //    {
        //        return await _context.Set<TEntity>().ToListAsync();
        //    }

        //    [HttpGet("{id}")]
        //    public virtual async Task<IActionResult> GetById(int id)
        //    {
        //        var entity = await _context.Set<TEntity>().FirstOrDefaultAsync();

        //        if (entity == null)
        //        {
        //            return NotFound();
        //        }

        //        return Ok(entity);
        //    }

        //    [HttpPost]
        //    public virtual async Task<IActionResult> Create([FromBody] TEntity entity)
        //    {
        //        _context.Set<TEntity>().Add(entity);
        //        await _context.SaveChangesAsync();

        //        return Ok(entity);
        //    }

        //    [HttpPut("{id}")]
        //    public virtual async Task<IActionResult> Update(int id, [FromBody] TEntity entity)
        //    {
        //        var ent = await _context.Set<TEntity>().FindAsync(id);
        //        if(ent == null) return NotFound();
        //        _context.Entry(entity).CurrentValues.SetValues(entity);
        //        await _context.SaveChangesAsync();
        //        return Ok(entity);
        //    }

        //    [HttpDelete("{id}")]
        //    public virtual async Task<IActionResult> Delete(int id)
        //    {
        //        var entity = await _context.Set<TEntity>().FindAsync(id);

        //        if (entity == null)
        //        {
        //            return NotFound();
        //        }

        //        _context.Set<TEntity>().Remove(entity);
        //        await _context.SaveChangesAsync();

        //        return NoContent();
        //    }

    }
}

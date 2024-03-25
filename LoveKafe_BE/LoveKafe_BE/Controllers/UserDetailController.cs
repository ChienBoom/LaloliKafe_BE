using LoveKafe_BE.Auth;
using LoveKafe_BE.Models;
using Microsoft.AspNetCore.Mvc;

namespace LoveKafe_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserDetailController : Controller
    {
        private AppDbContext _context;
        public UserDetailController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<UserDetail> userDetails = _context.UserDetail.ToList();
            return Ok(new ResultData<UserDetail> { TotalCount = userDetails.Count, Data = userDetails });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            UserDetail userDetail = _context.UserDetail.FirstOrDefault(c => c.Id == id);
            if (userDetail == null) return NotFound();
            return Ok(userDetail);
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserDetail value)
        {
            _context.UserDetail.Add(value);
            _context.SaveChanges();
            return Ok(value);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] UserDetail value)
        {
            UserDetail userDetail = _context.UserDetail.FirstOrDefault(o => o.Id == id);
            if (userDetail == null) return NotFound();
            _context.Entry(userDetail).CurrentValues.SetValues(value);
            _context.SaveChangesAsync();
            return Ok(value);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            UserDetail userDetail = _context.UserDetail.FirstOrDefault(o => o.Id == id);
            if (userDetail == null) return NotFound();
            _context.UserDetail.Remove(userDetail);
            return Ok();
        }
    }
}

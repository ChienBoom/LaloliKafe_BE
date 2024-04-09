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
    public class UserDetailController : Controller
    {
        private AppDbContext _context;
        private Util _util;
        public UserDetailController(AppDbContext context, Util util)
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

            if (queryParams.TryGetValue("role", out string role) && role != null) queryModels.Add("role", role);
            
            var query = _context.UserDetail.AsQueryable();
            foreach (var param in queryModels)
            {
                query = query.Where(param.Key + " == @0", param.Value);
            }
            List<UserDetail> userDetails = query.ToList();
            return Ok(new ResultData<UserDetail> { TotalCount = userDetails.Count, Data = userDetails });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            UserDetail userDetail = _context.UserDetail.FirstOrDefault(c => c.Id == id);
            if (userDetail == null) return NotFound();
            return Ok(userDetail);
        }

        //[Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] UserDetail value)
        {
            _context.UserDetail.Add(value);
            _context.SaveChanges();
            return Ok(value);
        }

        //[Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] UserDetail value)
        {
            UserDetail userDetail = _context.UserDetail.FirstOrDefault(o => o.Id == id);
            if (userDetail == null) return NotFound();
            _context.Entry(userDetail).CurrentValues.SetValues(value);
            _context.SaveChangesAsync();
            return Ok(value);
        }

        //[Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            UserDetail userDetail = _context.UserDetail.FirstOrDefault(o => o.Id == id);
            if (userDetail == null) return NotFound();
            _context.UserDetail.Remove(userDetail);
            _context.SaveChangesAsync();
            return Ok();
        }
    }
}

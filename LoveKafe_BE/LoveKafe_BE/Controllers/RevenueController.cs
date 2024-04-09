using LoveKafe_BE.Auth;
using LoveKafe_BE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoveKafe_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RevenueController : ControllerBase
    {
        private AppDbContext _context;
        public RevenueController(AppDbContext context)
        {
            _context = context;
        }

        //[Authorize]
        [HttpGet("month")]
        public IActionResult GetDataTypeMonth()
        {
            List<string> labels =new List<string>() { "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12" };
            string label = "VNĐ";
            List<decimal> revenues = new List<decimal>();
            for (int i=1; i<=12; i++)
            {
                var query = from orderDetail in _context.OrderDetail
                            join order in _context.Order on orderDetail.OrderId equals order.Id
                            where order.OrderDate.Month == i
                            join product in _context.Product on orderDetail.ProductId equals product.Id
                            select new { orderDetail , product};
                var result = query.ToList();
                decimal totalPrice = result.Sum(o => o.product.Price * o.orderDetail.Quantity);
                revenues.Add(totalPrice);
            }
            return Ok(new Revenue { Labels = labels, Revenues = revenues, Label = label});
        }

        //[Authorize]
        [HttpGet("category")]
        public IActionResult GetDataTypeCategory()
        {

            List<string> labels = new List<string>();
            string label = "VNĐ";
            List<decimal> revenues = new List<decimal>();

            List<Category> categories = _context.Category.Where(o => o.IsDelete == 0).ToList();
            var query = from orderDetail in _context.OrderDetail
                        join product in _context.Product on orderDetail.ProductId equals product.Id
                        join cate in _context.Category on product.CategoryId equals cate.Id
                        select new { orderDetail, product, cate };
            var result = query.ToList().GroupBy(o => o.cate).ToList();
            foreach(var rs in result)
            {
                Category ca = rs.Key;
                labels.Add(ca.Name);
                decimal totalPrice = rs.Sum(o => o.product.Price * o.orderDetail.Quantity);
                revenues.Add(totalPrice);
            }
            return Ok(new Revenue { Labels = labels, Revenues = revenues, Label = label });
        }

        //[Authorize]
        [HttpGet("product")]
        public IActionResult GetDataTypeProduct()
        {

            List<string> labels = new List<string>();
            string label = "VNĐ";
            List<decimal> revenues = new List<decimal>();

            List<Category> categories = _context.Category.Where(o => o.IsDelete == 0).ToList();
            var query = from orderDetail in _context.OrderDetail
                        join product in _context.Product on orderDetail.ProductId equals product.Id
                        select new { orderDetail, product };
            var result = query.ToList().GroupBy(o => o.product).ToList();
            foreach (var rs in result)
            {
                Product product = rs.Key;
                labels.Add(product.Name);
                decimal totalPrice = rs.Sum(o => o.product.Price * o.orderDetail.Quantity);
                revenues.Add(totalPrice);
            }
            return Ok(new Revenue { Labels = labels, Revenues = revenues, Label = label });
        }

    }
}

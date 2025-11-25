using EcomSales.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcomSales.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevenueController : ControllerBase
    {
        private readonly AppDbContext _Db;

        public RevenueController(AppDbContext db)
        {
            _Db = db;
        }

        [HttpGet("total")]
        public async Task<IActionResult> GetTotalRevenue([FromQuery] DateTime? fromDt, [FromQuery] DateTime? toDt)
        {
            var q = from oi in _Db.OrderItems
                    join o in _Db.Orders on oi.Id equals o.Id
                    where (!fromDt.HasValue || o.DateOfSale >= fromDt) &&
                    (!toDt.HasValue || o.DateOfSale <= toDt)
                    select (oi.UnitPrice * oi.Quantity);
            var _total = await q.SumAsync();

            return Ok(new TotalRevenueDto{
                Total= _total
            });
        }

        [HttpGet("by-product")]
        public async Task<IActionResult> GetTotalRevenueByProduct([FromQuery] int ProductId, [FromQuery] DateTime? fromDt, [FromQuery] DateTime? toDt)
        {
            var q = from oi in _Db.OrderItems
                    join o in _Db.Orders on oi.Id equals o.Id
                    join p in _Db.Products on oi.ProductId equals ProductId
                    where (!fromDt.HasValue || o.DateOfSale >= fromDt) &&
                    (!toDt.HasValue || o.DateOfSale <=toDt)
                    select (oi.UnitPrice * oi.Quantity);
            var total = await q.SumAsync();
            return Ok(new TotalRevenueDto
            {
                Total = total
            });
        }

        [HttpGet("by-category")]
        public async Task<IActionResult> GetTotalRevenueByCategory([FromQuery] string CategoryName, [FromQuery] DateTime? fromDt, [FromQuery] DateTime? toDt)
        {
            var q = from oi in _Db.OrderItems
                    join o in _Db.Orders on oi.Id equals o.Id
                    where (!fromDt.HasValue || o.DateOfSale >= fromDt) &&
                    (!toDt.HasValue || o.DateOfSale <= toDt) && (oi.Product.Category == CategoryName)
                    select (oi.UnitPrice * oi.Quantity);
            var total = await q.SumAsync();
            return Ok(new TotalRevenueDto
            {
                Total = total
            });
        }

        [HttpGet("by-region")]
        public async Task<IActionResult> GetTotalRevenueByRegion([FromQuery] string Region, [FromQuery] DateTime? fromDt, [FromQuery] DateTime? toDt)
        {
            var q = from oi in _Db.OrderItems
                    join o in _Db.Orders on oi.Id equals o.Id
                    where (!fromDt.HasValue || o.DateOfSale >= fromDt) &&
                    (!toDt.HasValue || o.DateOfSale <= toDt) && (o.Region == Region)
                    select (oi.UnitPrice * oi.Quantity);
            var total = await q.SumAsync();
            return Ok(new TotalRevenueDto
            {
                Total = total
            });
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertySystem.Data;
using PropertySystem.Models;

namespace PropertySystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly PropertyContext _context;

        public HomeController(PropertyContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SearchProperties([FromBody] PropertySearchFilter filter)
        {
            var query = _context.Properties.AsQueryable();

            // 应用过滤条件
            if (!string.IsNullOrEmpty(filter.Region))
            {
                query = query.Where(p => p.Region == filter.Region);
            }

            if (!string.IsNullOrEmpty(filter.District))
            {
                query = query.Where(p => p.District == filter.District);
            }

            if (!string.IsNullOrEmpty(filter.PropertyType))
            {
                query = query.Where(p => p.PropertyType == filter.PropertyType);
            }

            if (filter.MinPrice.HasValue)
            {
                query = query.Where(p => p.SalePrice >= filter.MinPrice.Value);
            }

            if (filter.MaxPrice.HasValue)
            {
                query = query.Where(p => p.SalePrice <= filter.MaxPrice.Value);
            }

            // 计算总数
            var totalCount = await query.CountAsync();

            // 应用分页
            var properties = await query
                .OrderBy(p => p.Id)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            var result = new PagedResult<Property>
            {
                Items = properties,
                TotalCount = totalCount,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            };

            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetFilterOptions()
        {
            var regions = await _context.Properties.Select(p => p.Region).Distinct().ToListAsync();
            var districts = await _context.Properties.Select(p => p.District).Distinct().ToListAsync();
            var propertyTypes = await _context.Properties.Select(p => p.PropertyType).Distinct().ToListAsync();

            return Json(new
            {
                regions = regions,
                districts = districts,
                propertyTypes = propertyTypes
            });
        }
    }
}
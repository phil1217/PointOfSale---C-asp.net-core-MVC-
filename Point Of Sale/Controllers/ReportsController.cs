using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Point_Of_Sale.Data;

namespace Point_Of_Sale.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SalesReports()
        {
            var reports = await _context.Reports.ToListAsync();

            return View(reports);
        }

        public async Task<IActionResult> InventoryReports()
        {
            var reports = await _context.Reports.ToListAsync();

            return View(reports);
        }
    }
}

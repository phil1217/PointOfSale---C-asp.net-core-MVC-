using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Point_Of_Sale.Data;
using Point_Of_Sale.Models;

namespace Point_Of_Sale.Controllers
{
    public class VendorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VendorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var vendors = await _context.Suppliers.ToListAsync();
            return View(vendors);
        }

        public IActionResult AddNewVendor()
        {

            return View();
        }

        public async Task<IActionResult> Add(Suppliers supplier)
        {
            if (supplier == null)
            {
                return BadRequest();
            }

            DateTime now = DateTime.Now;
            string dateString = now.ToString("yyyy-MM-dd HH:mm:ss");

            supplier.CreatedAt = dateString;
            supplier.UpdatedAt = dateString;

            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> EditVendor(string? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var vendor = await _context.Suppliers.FindAsync(id);
            if (vendor == null)
            {
                return NotFound();
            }

            return View(vendor);
        }

        public async Task<IActionResult> Edit(Suppliers supplier)
        {
            if (supplier == null)
            {
                return BadRequest();
            }

            DateTime now = DateTime.Now;
            string dateString = now.ToString("yyyy-MM-dd HH:mm:ss");

            supplier.UpdatedAt = dateString;

            _context.Suppliers.Update(supplier);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ViewVendor(string? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var vendor = await _context.Suppliers.FindAsync(id);
            if (vendor == null)
            {
                return NotFound();
            }

            return View(vendor);
        }

        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var vendor = await _context.Suppliers.FindAsync(id);
            if(vendor == null)
            {
                return NotFound();
            }

            _context.Suppliers.Remove(vendor);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}

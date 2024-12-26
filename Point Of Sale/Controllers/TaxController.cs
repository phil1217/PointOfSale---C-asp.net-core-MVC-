using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Point_Of_Sale.Data;
using Point_Of_Sale.Models;

namespace Point_Of_Sale.Controllers
{
    public class TaxController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaxController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var taxRules = await _context.TaxRules.ToListAsync();
            return View(taxRules ?? new List<TaxRules>());
        }

        public async Task<IActionResult> Add(TaxRules taxRules)
        {
            if (taxRules == null)
            {
                return BadRequest();
            }

            _context.TaxRules.Add(taxRules);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult AddNewRule()
        {
            return View();
        }

        public async Task<IActionResult> EditTax(string? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var taxRule = await _context.TaxRules.FindAsync(id);
            if(taxRule == null)
            {
                return NotFound();
            }
            return View(taxRule);
        }

        public async Task<IActionResult> Edit(TaxRules taxRule)
        {
            if(taxRule == null)
            {
                return BadRequest();
            }

            _context.TaxRules.Update(taxRule);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ViewTax(string? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var taxRule = await _context.TaxRules.FindAsync(id);
            if(taxRule == null)
            {
                return NotFound();
            }
            return View(taxRule);
        }

        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var taxRule = await _context.TaxRules.FindAsync(id);
            if(taxRule == null)
            {
                return NotFound();
            }

            _context.TaxRules.Remove(taxRule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

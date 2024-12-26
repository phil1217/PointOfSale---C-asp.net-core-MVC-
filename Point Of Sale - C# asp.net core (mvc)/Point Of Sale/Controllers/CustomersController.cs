using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Point_Of_Sale.Data;
using Point_Of_Sale.Models;

namespace Point_Of_Sale.Controllers
{
    public class CustomersController : Controller
    {

        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var customers = await _context.Customers.ToListAsync();
            return View(customers ?? new List<Customers>());
        }

        public IActionResult AddNewCustomer()
        {
            return View();
        }

       
        public async Task<IActionResult> EditCustomer(string? id)
        {
            var customer = await _context.Customers.FindAsync(id); 
            if (customer == null)
            {
                return NotFound(); 
            }
            return View(customer);
        }

        public async Task<IActionResult> ViewCustomer(string? id)
        {
            var customers = await _context.Customers.FindAsync(id);
            return View(customers);
        }

        public async Task<IActionResult> Add(Customers? customer)
        {
            if (customer == null)
            {
                return BadRequest("Customer data is required.");
            }
            DateTime now = DateTime.Now;
            string dateString = now.ToString();
            customer.CreatedAt = dateString;
            customer.UpdatedAt = dateString;

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Customers? customer)
        {
            if (customer == null)
            {
                return BadRequest("Customer data is required.");
            }

            DateTime now = DateTime.Now;
            string dateString = now.ToString();
            customer.UpdatedAt = dateString;

            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Customer ID is required.");
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound("Customer not found.");
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}

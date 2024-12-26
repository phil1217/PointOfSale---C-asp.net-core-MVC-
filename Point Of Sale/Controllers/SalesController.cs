using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Point_Of_Sale.Data;
using Point_Of_Sale.Models;

namespace Point_Of_Sale.Controllers
{
    public class SalesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders.ToListAsync();

            var sales = new List<SalesViewModel>();
            foreach (var order in orders)
            {
                var sale = new SalesViewModel();
                sale.Orders = order;

                var customer = await _context.Customers.FindAsync(order.CustomerID);

                if (customer == null)
                {
                    customer = new Customers();
                }

                sale.Customers = customer;

                sales.Add(sale);
            }

            return View(sales);
        }
        
        public IActionResult CreateOrder()
        {
            ViewBag.CustomerID = _context.Customers
                .Select(c => new SelectListItem()
                {
                    Value = c.CustomerID,
                    Text = c.FirstName + " " + c.LastName
                }).ToList();

            ViewBag.Tax = _context.TaxRules
             .Where(t => t.TaxRate > 0)
             .Select(t => new SelectListItem
             {
                 Value = t.TaxRate.ToString(),
                 Text = t.TaxName
             })
             .ToList();

            ViewBag.Discount = _context.TaxRules
             .Where(t => t.TaxRate < 0)
             .Select(t => new SelectListItem
             {
                 Value = t.TaxRate.ToString(),
                 Text = t.TaxName
             })
             .ToList();

            ViewBag.ProductID = _context.Products
             .Select(t => new SelectListItem
             {
                 Value = t.ProductID.ToString(),
                 Text = t.ProductName
             })
             .ToList();

            return View();
        }

        public async Task<IActionResult> Create(SalesAddModel sales)
        {
            if (sales == null)
            {
                return BadRequest();
            }

            var order = await _context.Orders.FindAsync(sales.Orders.OrderID); 

            if (order == null)
            {
                _context.Orders.Add(sales.Orders);
            }
            else
            {
                _context.Entry(order).State = EntityState.Detached;
                _context.Orders.Update(sales.Orders);
            }

            sales.OrderDetails.OrderID = sales.Orders.OrderID;

            _context.OrderDetails.Add(sales.OrderDetails);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ViewOrder(string? id)
        {
            var sales = new ViewOrderModel();
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            var orderDetails = await _context.OrderDetails.Where(o => o.OrderID == order.OrderID).ToListAsync();
            
            sales.Orders = order;
            sales.OrderDetailsList = orderDetails;

            return View(sales);
        }

        public async Task<IActionResult> Cancel(string? id)
        {
            if(id == null)
            {
                return BadRequest();
            }

            var sale = await _context.Orders.FindAsync(id);

            if (sale == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(sale);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
